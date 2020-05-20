namespace PaymentGateway.WebApi.Models.v1
{
    public class PaymentRequest
    {
        public string MerchantReferenceNumber { get; set; }
        public string LongCardNumber { get; set; }
        public string NameOnCard { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public int CVV { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
    }
}
