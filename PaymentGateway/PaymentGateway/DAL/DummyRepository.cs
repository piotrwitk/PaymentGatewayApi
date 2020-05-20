using PaymentGateway.Models;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.DAL
{
    public class DummyRepository : IRepository
    {
        public Task RegisterResponse(GatewayResponse response)
        {
            throw new NotImplementedException();
        }

        public Task<GatewayResponse> RetrieveDetails(string merchantId, string merchantRef)
        {
            throw new NotImplementedException();
        }

        public Task<GatewayPaymentRequest> RetrieveRequest(string gatewayId)
        {
            throw new NotImplementedException();
        }

        Task<GatewayPaymentRequest> IRepository.RegisterPaymentRequest(GatewayPaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
