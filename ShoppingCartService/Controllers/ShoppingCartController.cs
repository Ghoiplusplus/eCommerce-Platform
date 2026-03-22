using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Repository;

namespace ShoppingCartService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly RedisRepository cartCache;
        private readonly ILogger<CartController> logger;

        public CartController(RedisRepository cartCache, ILogger<CartController> logger)
        {
            this.cartCache = cartCache;
            this.logger = logger;
        }

        private string userId => HttpContext.Items["UserId"].ToString();

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cartList = await cartCache.GetAllAsync(userId);
            if (cartList == null)
            {
                logger.LogInformation("{userId} cart empty", userId);
                return Ok(new List<object>());
            }

            var response = cartList.Select(x => new
            {
                ProductId = (int)x.Name,
                Quantity = (int)x.Value
            });

            logger.LogInformation("Found {userId} cart", userId);

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateQuantityAsync(int ProductId, int Amount = 1)
        {
            await cartCache.AddOrUpdateAsync(userId, ProductId, Amount);

            logger.LogInformation("Updated {userId} item {productId}", userId, ProductId);

            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int ProductId)
        {
            await cartCache.DeleteAsync(userId, ProductId);
            logger.LogInformation("Deleted from {userId} item {productId}", userId, ProductId);
            return Ok();
        }
    }
}
