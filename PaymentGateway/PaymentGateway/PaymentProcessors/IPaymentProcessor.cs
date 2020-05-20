using PaymentGateway.Models;
using System.Threading.Tasks;

namespace PaymentGateway.PaymentProcessors
{
    public interface IPaymentProcessor
    {
        Task<GatewayResponse> HandlePaymentRequest(GatewayPaymentRequest request);
    }
}
