using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace NZwalks.api.Repositories
{
    public class SqlTokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public SqlTokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //Create Claims
            var cliams = new List<Claim>();
            cliams.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach(var role in roles)
            {
                cliams.Add(new Claim(ClaimTypes.Role, role));
            }
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"]
                , cliams, expires:DateTime.Now.AddMinutes(15),signingCredentials: Credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
