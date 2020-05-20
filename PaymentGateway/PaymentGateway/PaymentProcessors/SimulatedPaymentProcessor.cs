using PaymentGateway.PaymentProcessors.Models;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.PaymentProcessors
{
    public class SimulatedPaymentProcessor : IPaymentProcessor
    {
        public Task<PaymentProcessorResponse> HandlePaymentRequest(PaymentProcessorRequest request)
        {
            return Task.FromResult(new PaymentProcessorResponse 
            {
                GatewayId = request.GatewayId,
                IsSuccess = true,
                FailureReason = string.Empty,
                PaymentProcessorReference = Guid.NewGuid().ToString()
            });
        }
    }
}
