using Microsoft.EntityFrameworkCore;
using ProductCatalog.Model;

namespace ProductCatalog.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductModel> products { get; set; } = null!;

        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        { 
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgresDB;Port=5432;Database=productsdb;Username=postgres;Password=123456");
        }
    }
}
