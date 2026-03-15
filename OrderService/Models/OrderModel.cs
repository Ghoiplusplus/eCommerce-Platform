using System.Text.Json.Serialization;

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

    public class OrderModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;
        [JsonIgnore]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
