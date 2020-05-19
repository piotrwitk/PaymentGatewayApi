namespace PaymentGateway.Models
{
    public class CardDetails
    {
        public string NameOnCard { get; set; }

        public string LongNumber { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }

        public int CVV { get; set; }
    }
}
