using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.PaymentProcessors.Models;
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
            var response = paymentProcessor.HandlePaymentRequest(new PaymentProcessorRequest { });
            return Task.FromResult(new GatewayPaymentResponse { });

        }
    }
}
