using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Repository;

namespace ShoppingCartService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly RedisRepository cartCache;

        public CartController(RedisRepository cartCache)
        {
            this.cartCache = cartCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            string? token = HttpContext.Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var cartList = await cartCache.GetAllAsync(token.Substring("Bearer ".Length));
            if (cartList == null) return Ok(new List<object>());

            var response = cartList.Select(x => new
            {
                ProductId = (int)x.Name,
                Quantity = (int)x.Value
            });
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateQuantityAsync(int ProductId, int Amount = 1)
        {
            string? token = HttpContext.Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            await cartCache.AddOrUpdateAsync(token.Substring("Bearer ".Length), ProductId, Amount);
            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int ProductId)
        {
            string? token = HttpContext.Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            await cartCache.DeleteAsync(token.Substring("Bearer ".Length), ProductId);
            return Ok();
        }
    }
}
