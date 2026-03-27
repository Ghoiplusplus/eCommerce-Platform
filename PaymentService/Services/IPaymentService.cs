namespace PaymentService.Services
{
    public interface IPaymentService<T> where T : class
    {
        T CreatePayment(int amount);
    }
}
