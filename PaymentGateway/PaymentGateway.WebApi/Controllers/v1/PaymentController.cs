using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.WebApi.Models.v1;
using System.Threading.Tasks;

namespace PaymentGateway.WebApi.Controllers.v1
{
    [Controller]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IGateway gateway;
        private readonly ISystemClock systemClock;

        public PaymentController(IGateway gateway, ISystemClock systemClock)
        {
            this.gateway = gateway;
            this.systemClock = systemClock;
        }

        [HttpGet("{merchantId}/{merchantReferenceNumber}")]
        public async Task<PaymentResponse> Details(string merchantId, string merchantReferenceNumber)
        {
            return new PaymentResponse { };
        }
    }
}
