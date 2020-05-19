namespace PaymentGateway.Models
{
    public class GatewayPaymentRequest
    {
        /// <summary>
        /// Id of the request as identified in our system
        /// </summary>
        public string GatewayId { get; set; }

        /// <summary>
        /// The ID under which a calling merchant is registered in our system
        /// </summary>
        public string MerchantId { get; set; }
        
        /// <summary>
        /// Merchant's own reference number for this call / transaction.
        /// This should be used to retrieve payment details on merchant's request
        /// </summary>        
        public string MerchantReferenceNumber { get; set; }

        /// <summary>
        /// Currency symbol and amount
        /// </summary>
        public PaymentCurrency PaymentCurrecny { get; set; }

        /// <summary>
        /// All details from a card needed to make a payment
        /// </summary>
        public CardDetails CardDetails { get; set; }
    }
}
