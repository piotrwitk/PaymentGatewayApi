namespace PaymentGateway.Models
{
    public class GatewayPaymentResponse
    {
        /// <summary>
        /// Indicates if the corresponding payment request was accepted by the payment provider on not
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
