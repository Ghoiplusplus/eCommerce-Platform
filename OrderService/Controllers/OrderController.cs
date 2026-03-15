using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.DTO;
using OrderService.Models;
using System.Security.Claims;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrderController : ControllerBase
    {
        private OrderContext orderContext;

        public OrderController(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderModel order)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("Empty GUID");
            }
            order.UserId = new Guid(userId);
            orderContext.Add(order);
            await orderContext.SaveChangesAsync();
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("Empty GUID");
            }
            var orders = await orderContext.Users
                .Where(u => u.UserId.ToString() == userId)
                .ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("Empty GUID");
            }

            var order = await orderContext.Users
                .Where(u => u.UserId.ToString() == userId && u.Id == id)
                .ToListAsync();

            return Ok(order);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UdpateOrderAsync(int id, [FromBody] OrderDTO newOrder)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("Empty GUID");
            }

            var orderToUpdate = orderContext.Users.FirstOrDefault(u => u.Id == id && u.UserId.ToString() == userId);
            if (orderToUpdate == null) return BadRequest("Bad order id");
            orderContext.Users.Entry(orderToUpdate).CurrentValues.SetValues(newOrder);

            await orderContext.SaveChangesAsync();

            return Ok("Updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> GetOrders(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("Empty GUID");
            }

            var orderToDelete = orderContext.Users.FirstOrDefault(u => u.Id == id && u.UserId.ToString() == userId);
            orderContext.Remove(orderToDelete);
            await orderContext.SaveChangesAsync();

            return Ok("Deleted");
        }
    }
}
