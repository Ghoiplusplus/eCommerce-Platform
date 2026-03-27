namespace PaymentService.Settings
{
    public class YookassaSettings
    {
        public string ServiceID { get; init; } = Environment.GetEnvironmentVariable("YOOKASSA_ID");
        public string ServiceSecret { get; init; } = Environment.GetEnvironmentVariable("YOOKASSA_SECRET_KEY");
    }
}
