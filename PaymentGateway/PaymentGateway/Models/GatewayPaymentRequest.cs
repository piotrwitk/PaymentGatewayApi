namespace PaymentGateway.Models
{
    public class GatewayPaymentRequest : BaseRequest
    {
        /// <summary>
        /// Id of the request as identified in our system
        /// </summary>
        public string GatewayId { get; set; }

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
