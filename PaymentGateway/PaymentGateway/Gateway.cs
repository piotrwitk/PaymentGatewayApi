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

        public Task<GatewayPaymentResponse> HandlePaymentRequest(GatewayPaymentRequest request)
        {
            var response = paymentProcessor.HandlePaymentRequest(request);
            return Task.FromResult(new GatewayPaymentResponse { IsSuccess = true });
        }

        public Task<GatewayDetailsResponse> HandleDetailsRequest(GatewayDetailsRequest request)
        {
            return Task.FromResult(new GatewayDetailsResponse { IsSuccess = true });
        }
    }
}
