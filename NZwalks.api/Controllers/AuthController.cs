using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.api.Models.DTO;
using NZwalks.api.Repositories;

namespace NZwalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Create([FromBody] registerRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Name,
                Email = registerRequestDto.Name
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.password);
            if (identityResult.Succeeded)
            {
                // add roles
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("user register ! please login");
                    }

                }
            }

            return BadRequest("something went wrong");


        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
                var check = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (check)
                {
                    //Get Roles From User
                    var roles = await userManager.GetRolesAsync(user);
                    //Create Token
                    var JwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
                    var response = new LoginResponseDto
                    {
                        JwtToken = JwtToken
                    };

                    return Ok(response);
                }

            }
            return BadRequest("incorrect userName or password");
        }
    }
}
