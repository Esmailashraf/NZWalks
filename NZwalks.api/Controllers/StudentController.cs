using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZwalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
    public IActionResult GetAllStudent()
        {
            string[] students = ["esmail", "ahmed", "ali", "mohamed"];

            return Ok(students);// return http with status 200

        }
    }
}
