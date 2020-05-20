using PaymentGateway.Models;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public interface IGateway
    {
        Task<GatewayPaymentResponse> HandlePaymentRequest(GatewayPaymentRequest request);
        Task<GatewayDetailsResponse> HandleDetailsRequest(GatewayDetailsRequest request);
    }
}
