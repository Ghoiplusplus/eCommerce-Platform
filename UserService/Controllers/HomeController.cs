using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [HttpGet]
        public string Index()
        {
            return "Hello World!";
        }
    }
}
