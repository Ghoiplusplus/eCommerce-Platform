using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data
{
    public class OrderContext : DbContext
    {
        public DbSet<OrderModel> Users { get; set; } = null!;
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
