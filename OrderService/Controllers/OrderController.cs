using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.DTO;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrderController : ControllerBase
    {
        private OrderContext _orderContext;
        private ILogger<OrderController> _logger;

        public OrderController(OrderContext orderContext, ILogger<OrderController> logger)
        {
            _orderContext = orderContext;
            _logger = logger;
        }
        string userId => HttpContext.Request.Headers["UserId"].ToString();

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderModel order)
        {
            order.UserId = new Guid(userId);

            _orderContext.Add(order);
            await _orderContext.SaveChangesAsync();

            _logger.LogInformation("Created new order for {UserId}", userId);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = await _orderContext.Users
                .Where(u => u.UserId.ToString() == userId)
                .ToListAsync();

            _logger.LogInformation("Get {UserId} orders", userId);

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var order = await _orderContext.Users
                .Where(u => u.UserId.ToString() == userId && u.Id == id)
                .ToListAsync();

            _logger.LogInformation("Get {Id} order from {UserId}", id, userId);

            return Ok(order);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UdpateOrderAsync(int id, [FromBody] OrderDTO newOrder)
        {
            var orderToUpdate = _orderContext.Users.FirstOrDefault(u => u.Id == id && u.UserId.ToString() == userId);
            if (orderToUpdate == null)
            {
                _logger.LogInformation("No {Id} order from {UserId}", id, userId);
                return BadRequest("Bad order id");
            }

            _orderContext.Users.Entry(orderToUpdate).CurrentValues.SetValues(newOrder);
            await _orderContext.SaveChangesAsync();

            _logger.LogInformation("Updated {Id} order of {UserId}", id, userId);

            return Ok("Updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrdersAsync(int id)
        {
            var orderToDelete = _orderContext.Users.FirstOrDefault(u => u.Id == id && u.UserId.ToString() == userId);
            if (orderToDelete == null)
            {
                _logger.LogInformation("No {Id} order from {UserId}", id, userId);
                return BadRequest("Bad order id");
            }

            _orderContext.Remove(orderToDelete);
            await _orderContext.SaveChangesAsync();

            _logger.LogInformation("Deleted {Id} order of {UserId}", id, userId);

            return Ok("Deleted");
        }
    }
}
