using Demo.Application.Implementations.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSecurityTokenHandlerWrapper _jwtHandler;

        public AuthController(JwtSecurityTokenHandlerWrapper jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            // This is a simple example. In production, you would validate the user's credentials.
            if (login.Username == "angularApp") // Example credentials
            {
                var token = _jwtHandler.GenerateJwtToken("1", "User"); // Example userId and role
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials.");
        }
    }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
