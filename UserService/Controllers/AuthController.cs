using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtService jwtService;
        private readonly UserContext userContext;
        public AuthController(IJwtService jwtService, UserContext userContext)
        {
            this.jwtService = jwtService;
            this.userContext = userContext;
        }
        [HttpPost]
        public string register([FromBody] UserModel user)
        {
            userContext.Users.Add(user);
            return jwtService.GenerateJwtToken(user);
        }
    }
}
