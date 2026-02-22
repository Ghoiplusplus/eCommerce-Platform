using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Data;
using ProductCatalog.Model;

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

        [HttpPost]
        public async Task AddProduct([FromBody] ProductModel product)
        {
            productContext.Products.Add(product);
            await productContext.SaveChangesAsync();
        }

        [HttpGet]
        public async Task GetAll()
        {
            IQueryable products = productContext.Products;
            await HttpContext.Response.WriteAsJsonAsync(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ProductModel? product = await productContext.Products.FindAsync(id);
            if (product == null)
            {
                return new ObjectResult("Товар не найден") { StatusCode = 404 };
            }
            return new JsonResult(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productToUpdate, int id)
        {
            ProductModel? product = await productContext.Products.FindAsync(id);
            if (product == null)
            {
                return new ObjectResult("Товар не найден") { StatusCode = 404 };
            }
            productContext.Products.Entry(product).CurrentValues.SetValues(productToUpdate);
            await productContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ProductModel? product = await productContext.Products.FindAsync(id);
            if (product == null)
            {
                return new ObjectResult("Товар не найден") { StatusCode = 404 };
            }
            productContext.Products.Remove(product);
            await productContext.SaveChangesAsync();
            return Ok();
        }
    }
}
