using OrderService.Models;

namespace OrderService.DTO
{
    public class OrderDTO
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total;
        public OrderStatus Status { get; set; }
    }
}
