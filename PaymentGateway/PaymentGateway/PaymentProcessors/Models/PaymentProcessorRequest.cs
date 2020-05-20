namespace PaymentGateway.PaymentProcessors.Models
{
    public class PaymentProcessorRequest
    {
        public string PaymentCurrency { get; set; }
        public double PaymentAmount { get; set; }
        public string LongCardNumber { get; set; }
        public string NameOnCard { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public int CCV { get; set; }
    }
}
