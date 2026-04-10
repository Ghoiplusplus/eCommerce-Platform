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
        private OrderContext orderContext;

        public OrderController(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }
        string userId => HttpContext.Request.Headers["UserId"].ToString();

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderModel order)
        {
            order.UserId = new Guid(userId);

            orderContext.Add(order);
            await orderContext.SaveChangesAsync();

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = await orderContext.Users
                .Where(u => u.UserId.ToString() == userId)
                .ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var order = await orderContext.Users
                .Where(u => u.UserId.ToString() == userId && u.Id == id)
                .ToListAsync();

            return Ok(order);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UdpateOrderAsync(int id, [FromBody] OrderDTO newOrder)
        {
            var orderToUpdate = orderContext.Users.FirstOrDefault(u => u.Id == id && u.UserId.ToString() == userId);
            if (orderToUpdate == null) return BadRequest("Bad order id");

            orderContext.Users.Entry(orderToUpdate).CurrentValues.SetValues(newOrder);
            await orderContext.SaveChangesAsync();

            return Ok("Updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> GetOrders(int id)
        {
            var orderToDelete = orderContext.Users.FirstOrDefault(u => u.Id == id && u.UserId.ToString() == userId);
            if (orderToDelete == null) return BadRequest("Bad order id");

            orderContext.Remove(orderToDelete);
            await orderContext.SaveChangesAsync();

            return Ok("Deleted");
        }
    }
}
