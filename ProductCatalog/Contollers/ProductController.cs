using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDTO)
        {
            var existingCategories = await productContext.Categories
                .Where(c => productDTO.Categories.Contains(c.Name))
                .ToListAsync();

            var existingNames = existingCategories.Select(c => c.Name).ToList();
            var newNames = productDTO.Categories.Except(existingNames);

            foreach (var name in newNames)
            {
                existingCategories.Add(new ProductCategoryModel { Name = name });
            }

            var product = new ProductModel
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Categories = existingCategories
            };
            productContext.Products.Add(product);
            await productContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await productContext.Products
                    .Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Categories = p.Categories.Select(c => c.Name).ToList()
                    })
                    .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productContext.Products
                    .Where(p => p.Id == id)
                    .Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Categories = p.Categories.Select(c => c.Name).ToList()
                    })
                    .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound("Товар не найден");
            }
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productToUpdate, int id)
        {
            ProductModel? product = await productContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Товар не найден");
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
                return NotFound("Товар не найден");
            }
            productContext.Products.Remove(product);
            await productContext.SaveChangesAsync();
            return Ok();
        }
    }
}
