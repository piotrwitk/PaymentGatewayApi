using PaymentGateway.Models;
using System.Threading.Tasks;

namespace PaymentGateway
{
    public interface IGateway
    {
        Task<GatewayResponse> HandlePaymentRequest(GatewayPaymentRequest request);
        Task<GatewayResponse> HandleDetailsRequest(GatewayDetailsRequest request);
    }
}
