using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.PaymentProcessors.Models;
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

        public async Task<GatewayResponse> HandlePaymentRequest(GatewayPaymentRequest request)
        {
            var paymentRequest = PaymentProcessorMapper.MapRequest(request);
            var response = await paymentProcessor.HandlePaymentRequest(paymentRequest);
            return PaymentProcessorMapper.MapResponse(response, request);
        }

        public Task<GatewayResponse> HandleDetailsRequest(GatewayDetailsRequest request)
        {
            return Task.FromResult(new GatewayResponse { IsSuccess = true });
        }
    }
}
