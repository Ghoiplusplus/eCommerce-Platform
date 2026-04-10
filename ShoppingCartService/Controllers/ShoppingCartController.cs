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

        string UserId => HttpContext.Request.Headers["UserId"].ToString();

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cartList = await cartCache.GetAllAsync(UserId);
            if (cartList == null)
            {
                logger.LogInformation("{userId} cart empty", UserId);
                return Ok(new List<object>());
            }

            var response = cartList.Select(x => new
            {
                ProductId = (int)x.Name,
                Quantity = (int)x.Value
            });

            logger.LogInformation("Found {userId} cart", UserId);

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateQuantityAsync(int ProductId, int Amount = 1)
        {
            await cartCache.AddOrUpdateAsync(UserId, ProductId, Amount);

            logger.LogInformation("Updated {userId} item {productId}", UserId, ProductId);

            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int ProductId)
        {
            await cartCache.DeleteAsync(UserId, ProductId);
            logger.LogInformation("Deleted from {userId} item {productId}", UserId, ProductId);
            return Ok();
        }
    }
}
