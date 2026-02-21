using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Data;

namespace ProductCatalog.Contollers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext productContext;

        public ProductController(ProductContext productContext)
        {
            this.productContext = productContext; 
        }

        [HttpGet]
        public async Task GetAll()
        {
            IQueryable products = productContext.Products;
            await HttpContext.Response.WriteAsJsonAsync(products);
        }
    }
}
