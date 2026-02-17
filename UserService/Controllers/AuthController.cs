using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IJwtService jwtService;
        public AuthController(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }
        //public string Login([FromBody] UserModel user)
        //{

        //}
    }
}
