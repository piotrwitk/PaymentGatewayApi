using Microsoft.AspNetCore.Mvc;
using PaymentGateway.WebApi.Filters;
using PaymentGateway.WebApi.Models.v1;
using PaymentGateway.WebApi.Utils;
using System.Threading.Tasks;

namespace PaymentGateway.WebApi.Controllers.v1
{
    [Controller]
    [Route("api/v1/[controller]")]
    [ApiExceptionFilter]
    public class PaymentController : ControllerBase
    {
        private readonly IGateway gateway;
        private readonly IGatewayClock clock;

        public PaymentController(IGateway gateway, IGatewayClock clock)
        {
            this.gateway = gateway;
            this.clock = clock;
        }

        [HttpGet("{merchantId}/{merchantReferenceNumber}")]
        public async Task<PaymentResponse> PaymentDetails(string merchantId, string merchantReferenceNumber)
        {
            var request = RequestMapper.MapDetailsRequest(merchantId, merchantReferenceNumber, clock);
            var result = await gateway.HandleDetailsRequest(request);
            return ResponseMapper.MapResponse(result);
        }

        [HttpPost("{merchantId}")]
        public async Task<PaymentResponse> PaymentRequest(string merchantId, [FromBody] PaymentRequest payload)
        {
            var request = RequestMapper.MapPaymentRequest(merchantId, payload, clock);
            var result = await gateway.HandlePaymentRequest(request);
            return ResponseMapper.MapResponse(result);
        }
    }
}
