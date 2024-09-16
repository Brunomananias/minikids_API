using API_Minikids.Models;
using API_Minikids.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace API_Minikids.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(User user)
        {
            if (user.Username == "Bruno" && user.Password == "123456")
            {
                var token = TokenService.GenerateToken(new Models.Cliente());
                return Ok(token);
            }

            return BadRequest("Username or Password Invalido");
        }
    }
}
