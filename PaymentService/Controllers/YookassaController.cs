using Microsoft.AspNetCore.Mvc;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class YookassaController : ControllerBase
    {
        private YookassaService _yookassaService;
        private ILogger<YookassaController> _logger;

        public YookassaController(YookassaService yookassaService, ILogger<YookassaController> logger)
        {
            this._yookassaService = yookassaService;
            this._logger = logger;
        }
        string userId => HttpContext.Request.Headers["UserId"].ToString();

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromQuery] int amount)
        {
            string paymentUrl = _yookassaService.CreatePayment(amount);
            _logger.LogInformation("Created new payment for {userId}: {paymentUrl}", userId, paymentUrl);

            return Redirect(paymentUrl);
        }
    }
}
