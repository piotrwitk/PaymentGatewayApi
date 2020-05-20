using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public class Gateway : IGateway
    {
        private readonly IPaymentProcessor paymentProcessor;

        public Gateway(IPaymentProcessor paymentProcessor)
        {
            this.paymentProcessor = paymentProcessor;
        }

        public Task<GatewayResponse> HandlePaymentRequest(GatewayPaymentRequest request)
        {
            var response = paymentProcessor.HandlePaymentRequest(request);
            return Task.FromResult(new GatewayResponse { IsSuccess = true });
        }

        public Task<GatewayResponse> HandleDetailsRequest(GatewayDetailsRequest request)
        {
            return Task.FromResult(new GatewayResponse { IsSuccess = true });
        }
    }
}
