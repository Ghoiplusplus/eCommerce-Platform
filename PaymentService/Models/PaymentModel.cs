namespace PaymentService.Models
{
    public enum PaymentStatus
    {
        Pending,
        Success,
        Cancelled,
    }

    public class PaymentModel
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
