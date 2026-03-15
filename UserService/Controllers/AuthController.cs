using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService jwtService;
        private readonly UserContext userContext;
        public AuthController(IJwtService jwtService, UserContext userContext)
        {
            this.jwtService = jwtService;
            this.userContext = userContext;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            userContext.Users.Add(user);
            await userContext.SaveChangesAsync();
            return Ok(jwtService.GenerateJwtToken(user));
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            return Ok($"Пользователь авторизован \\n{HttpContext.Request.Headers.Authorization}");
        }
    }
}
