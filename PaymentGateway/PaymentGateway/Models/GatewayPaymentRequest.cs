namespace PaymentGateway.Models
{
    public class GatewayPaymentRequest : BaseRequest
    {
        /// <summary>
        /// Id of the request as identified in our system
        /// </summary>
        public string GatewayId { get; set; }

        /// <summary>        
        /// Currency symbol expressed in ISO 4217
        /// </summary>
        /// <see cref="https://en.wikipedia.org/wiki/ISO_4217"/>
        public string PaymentCurrency { get; set; }

        /// <summary>
        /// Amount of the request in currency defined by <see cref="PaymentCurrency"/>
        /// </summary>
        public double PaymentAmount { get; set; }

        public string NameOnCard { get; set; }

        public string LongNumber { get; set; }

        public string TruncatedNumber { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }

        public int CVV { get; set; }
    }
}
