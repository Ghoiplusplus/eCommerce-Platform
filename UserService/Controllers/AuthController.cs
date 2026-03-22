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
        private readonly ILogger<AuthController> logger;

        public AuthController(IJwtService jwtService, UserContext userContext, ILogger<AuthController> logger)
        {
            this.jwtService = jwtService;
            this.userContext = userContext;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            userContext.Users.Add(user);
            await userContext.SaveChangesAsync();

            logger.LogInformation("Added new user: Email={Email}, Guid={UserId}", user.Email, user.UserId);

            return Ok(jwtService.GenerateJwtToken(user));
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            return Ok($"Пользователь авторизован \\n{HttpContext.Request.Headers.Authorization}");
        }
    }
}
