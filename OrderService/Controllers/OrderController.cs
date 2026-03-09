using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync()
        {

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UdpateOrderAsync(int id)
        {
            return Ok("Updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> GetOrders(int id)
        {
            return Ok("Deleted");
        }
    }
}
