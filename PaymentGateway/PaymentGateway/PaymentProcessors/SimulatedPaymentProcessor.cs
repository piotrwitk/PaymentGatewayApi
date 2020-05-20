using PaymentGateway.Models;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.PaymentProcessors
{
    public class SimulatedPaymentProcessor : IPaymentProcessor
    {
        public Task<GatewayPaymentResponse> HandlePaymentRequest(GatewayPaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
