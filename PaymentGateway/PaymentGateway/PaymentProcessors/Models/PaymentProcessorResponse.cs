namespace PaymentGateway.PaymentProcessors.Models
{
    public class PaymentProcessorResponse
    {
        public string PaymentProcessorReference { get; set; }
        public string GatewayId { get; set; }
        public bool IsSuccess { get; set; }
        public string FailureReason { get; set; }
    }
}
