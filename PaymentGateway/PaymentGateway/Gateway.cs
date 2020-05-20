using PaymentGateway.DAL;
using PaymentGateway.Exceptions;
using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.PaymentProcessors.Models;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public class Gateway : IGateway
    {
        private readonly IPaymentProcessor paymentProcessor;
        private readonly IRepository repository;

        public Gateway(IPaymentProcessor paymentProcessor, IRepository repository)
        {
            this.paymentProcessor = paymentProcessor;
            this.repository = repository;
        }

        public async Task<GatewayResponse> HandlePaymentRequest(GatewayPaymentRequest request)
        {
            var requestWithId = await repository.RegisterPaymentRequest(request);

            var paymentRequest = PaymentProcessorMapper.MapRequest(requestWithId);
            var result = await paymentProcessor.HandlePaymentRequest(paymentRequest);
            var response = PaymentProcessorMapper.MapResponse(result, request);

            await repository.RegisterResponse(response);
            return response;
        }

        public async Task<GatewayResponse> HandleDetailsRequest(GatewayDetailsRequest request)
        {
            var response = await repository.RetrieveDetails(request.MerchantId, request.MerchantReferenceNumber);

            if (response == null)
            {
                throw new PaymentResponseNotFoundException();
            }

            return response;
        }
    }
}
