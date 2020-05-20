using PaymentGateway.Models;
using System.Threading.Tasks;

namespace PaymentGateway.DAL
{
    public interface IRepository
    {
        Task<GatewayPaymentRequest> RegisterPaymentRequest(GatewayPaymentRequest request);

        Task RegisterResponse(GatewayResponse response);

        Task<GatewayResponse> RetrieveDetails(string merchantId, string merchantRef);

        Task<GatewayPaymentRequest> RetrieveRequest(string gatewayId);
    }
}
