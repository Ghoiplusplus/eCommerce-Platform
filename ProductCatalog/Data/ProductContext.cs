using Microsoft.EntityFrameworkCore;
using ProductCatalog.Model;

namespace ProductCatalog.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; } = null!;
        public DbSet<ProductCategoryModel> Categories { get; set; } = null!;

        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        { 
            Database.EnsureDeleted();
            Database.EnsureCreated();

            ProductModel mock = new ProductModel() { 
                Name = "Test",
                Description = "Description test",
                ProductCategorys =
                {
                    new ProductCategoryModel()
                    {
                        Name = "Test category",
                    },
                    new ProductCategoryModel()
                    {
                        Name = "2nd test category"
                    }
                }
            };

            Products.AddRange(mock);
            SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgresDB;Port=5432;Database=productsdb;Username=postgres;Password=123456");
        }
    }
}
