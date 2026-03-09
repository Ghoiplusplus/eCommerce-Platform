namespace OrderService.Models
{
    public enum OrderStatus
    {
        Created,
        PendingPayment,
        Paid,
        Shipped,
        Cancelled
    }

    public class UsersOrderList
    {
        public Guid UserId { get; set; }
        public List<OrderModel> Orders { get; set; }
    }

    public class OrderModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public Guid UserId { get; set; }
    }
}
