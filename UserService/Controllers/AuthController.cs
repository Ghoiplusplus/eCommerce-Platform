using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IJwtService jwtService;
        private readonly UserContext userContext;
        public AuthController(IJwtService jwtService, UserContext userContext)
        {
            this.jwtService = jwtService;
            this.userContext = userContext;
        }
        [HttpPost("/api/auth")]
        public async Task register([FromBody] UserModel user)
        {
            userContext.Users.Add(user);
            await userContext.SaveChangesAsync();
            await HttpContext.Response.WriteAsJsonAsync(jwtService.GenerateJwtToken(user));
        }

        [HttpGet("/api/profile")]
        public async Task profile()
        {
            await HttpContext.Response.WriteAsJsonAsync($"Пользователь авторизован \n{HttpContext.Request.Body.ToString()}");
        }
    }
}
