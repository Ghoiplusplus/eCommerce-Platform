using PaymentService.Settings;
using Yandex.Checkout.V3;

namespace PaymentService.Services
{
    public class YookassaService : IPaymentService<string>
    {
        private YookassaSettings _paymentSettings { get; init; }
        private Client _client { get; init; }
        public string _returnUrl { get; set; } = "http://localhost:5202"; // TODO: добавить нормальный инжект ссылки

        public YookassaService()
        {
            _paymentSettings = new YookassaSettings();
            _client = new Client(_paymentSettings.ServiceID, _paymentSettings.ServiceSecret);
        }

        public string CreatePayment(int amount)
        {
            var newPayment = new NewPayment
            {
                Amount = new Amount { Value = amount, Currency = "RUB" },
                Confirmation = new Confirmation { Type = ConfirmationType.Redirect, ReturnUrl = _returnUrl }
            };
            Payment payment = _client.CreatePayment(newPayment);

            string url = payment.Confirmation.ConfirmationUrl;
            return url;
        }
    }
}
