using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingCart.Model;
using System.Text.Json;

namespace ShoppingCartService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IDistributedCache cartCache;

        public ShoppingCartController(IDistributedCache cartCache)
        {
            this.cartCache = cartCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            IEnumerable<ShoppingCartItem>? shoppingList = null;

            string? token = HttpContext.Request.Headers.Authorization;
            if (token == null)
            {
                return NotFound();
            }

            var cartList = await cartCache.GetAsync(token);
            if (cartList != null) shoppingList = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartList);

            return Ok(shoppingList);
        }
    }
}
