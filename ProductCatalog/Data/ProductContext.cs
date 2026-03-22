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
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>()
                .HasMany(c => c.Categories)
                .WithMany(p => p.Products)
                .UsingEntity(j => j.ToTable("ProductCategoryLinks"));
        }
    }
}
