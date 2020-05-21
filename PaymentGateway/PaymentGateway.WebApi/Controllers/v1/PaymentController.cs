using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.WebApi.Filters;
using PaymentGateway.WebApi.Models.v1;
using PaymentGateway.WebApi.Utils;
using Swashbuckle.AspNetCore.Annotations;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gateway"></param>
        /// <param name="clock"></param>
        public PaymentController(IGateway gateway, IGatewayClock clock)
        {
            this.gateway = gateway;
            this.clock = clock;
        }

        /// <summary>
        /// Retrieves payment request details
        /// </summary>
        /// <param name="merchantId">Id of the merchant from when registered in the system</param>
        /// <param name="merchantReferenceNumber">Merchant's own reference number. Must be unique per operation</param>
        /// <response code="200">Returns details of the payment operation registered in the system</response>
        /// <response code="400">Passed arguments are not correct</response>  
        /// <response code="404">Payment details for specific argument could not be found</response>  
        [HttpGet("{merchantId}/{merchantReferenceNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Retrieves payment request details", Description = "Only 4 last digits of the card used in operation will be shown")]
        [Produces("application/json")]
        public async Task<PaymentResponse> PaymentDetails(string merchantId, string merchantReferenceNumber)
        {
            var request = RequestMapper.MapDetailsRequest(merchantId, merchantReferenceNumber, clock);
            var result = await gateway.HandleDetailsRequest(request);
            return ResponseMapper.MapResponse(result);
        }

        /// <summary>
        /// Attempts to process a payment request
        /// </summary>
        /// <param name="merchantId">Id of the merchant from when registered in the system</param>
        /// <param name="payload">Payment request containing card's details, amount, currency</param>
        /// <response code="200">The payment request has been processed</response>
        /// <response code="400">Payment details are not correct</response>  
        /// <response code="404">Merchant could not be found and payment was not processed</response>  
        [HttpPost("{merchantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Attempts to process a payment request", Description = "Requires a merchant id as stored in the system and payment details payload")]
        public async Task<PaymentResponse> PaymentRequest(string merchantId, [FromBody] PaymentRequest payload)
        {
            var request = RequestMapper.MapPaymentRequest(merchantId, payload, clock);
            var result = await gateway.HandlePaymentRequest(request);
            return ResponseMapper.MapResponse(result);
        }
    }
}
