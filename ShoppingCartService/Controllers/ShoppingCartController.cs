using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingCart.Model;
using System.Text.Json;

namespace ShoppingCartService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IDistributedCache cartCache;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // Игнорировать регистр имен
            WriteIndented = false               // Для экономии места в кэше
        };

        public CartController(IDistributedCache cartCache)
        {
            this.cartCache = cartCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            IEnumerable<ShoppingCartItem>? shoppingList = null;

            string? token = HttpContext.Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var cartList = await cartCache.GetAsync(token.Substring("Bearer ".Length));
            if (cartList != null) shoppingList = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartList, _jsonOptions);

            return Ok(shoppingList);
        }
        [HttpPost]
        public async Task<IActionResult> AddCartItem(List<ShoppingCartItem> shoppingCartItems)
        {
            string? token = HttpContext.Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            await cartCache.SetStringAsync(token.Substring("Bearer ".Length), JsonSerializer.Serialize(shoppingCartItems));
            return Ok(shoppingCartItems);
        }
    }
}
