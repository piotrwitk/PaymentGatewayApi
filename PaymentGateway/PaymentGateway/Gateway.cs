using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public class Gateway
    {
        private readonly IPaymentProcessor paymentProcessor;

        public Gateway(IPaymentProcessor paymentProcessor)
        {
            this.paymentProcessor = paymentProcessor;
        }

        public Task<GatewayPaymentResponse> HandleIncomingPaymentRequest(GatewayPaymentRequest request)
        {
            var response = paymentProcessor.HandlePaymentRequest(request);
            return Task.FromResult(new GatewayPaymentResponse { });
        }
    }
}
