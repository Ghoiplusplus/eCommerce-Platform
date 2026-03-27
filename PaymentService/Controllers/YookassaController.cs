using Microsoft.AspNetCore.Mvc;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class YookassaController : ControllerBase
    {
        private YookassaService _yookassaService;
        private ILogger _logger;

        public YookassaController(YookassaService yookassaService, ILogger logger)
        {
            this._yookassaService = yookassaService;
            this._logger = logger;
        }

        string userId => HttpContext.Items["UserId"]?.ToString();
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] int amount)
        {
            string returnUrl = _yookassaService.CreatePayment(amount);
            _logger.LogInformation("Created new payment {userId}", userId);

            return Redirect(returnUrl);
        }
    }
}
