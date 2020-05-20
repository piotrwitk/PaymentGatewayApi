using PaymentGateway.PaymentProcessors.Models;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.PaymentProcessors
{
    /// <summary>
    /// Hardcoded, always happy handler
    /// </summary>
    public class SimulatedPaymentProcessor : IPaymentProcessor
    {
        public Task<PaymentProcessorResponse> HandlePaymentRequest(PaymentProcessorRequest request)
        {
            return Task.FromResult(new PaymentProcessorResponse 
            {
                IsSuccess = true,
                FailureReason = string.Empty,
                PaymentProcessorReference = Guid.NewGuid().ToString()
            });
        }
    }
}
