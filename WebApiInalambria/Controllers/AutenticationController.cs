using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPruebaInalambria.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace WebApiPruebaInalambria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;

        public AuthenticationController(IConfiguration config)
        {
            secretKey = config.GetSection("settings").GetValue<string>("secretKey") ?? throw new ArgumentNullException(nameof(secretKey));
        }
        [HttpPost]
        [Route("Validate")]
        public IActionResult Validate([FromBody] User request)
        {
            //se validan los parametros para poder crear tokens 
            if (request.Name == "Admin" && request.Password == "123")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Name));
                //se le agregan los parametros y el tiempo en el que se vence el token
                var token = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfiguration = tokenHandler.CreateToken(token);
                string CreateToken=tokenHandler.WriteToken(tokenConfiguration);
                return StatusCode(StatusCodes.Status200OK, new { token = CreateToken });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { token = "" });
            }
        }
    }
}
