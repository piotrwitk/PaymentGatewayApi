using PaymentGateway.Models;
using System.Threading.Tasks;

namespace PaymentGateway.PaymentProcessors
{
    public interface IPaymentProcessor
    {
        Task<GatewayPaymentResponse> HandlePaymentRequest(GatewayPaymentRequest request);
    }
}
