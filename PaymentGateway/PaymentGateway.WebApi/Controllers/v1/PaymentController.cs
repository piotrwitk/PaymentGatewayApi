﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<PaymentResponse> Details(string merchantId, string merchantReferenceNumber)
        {
            var request = RequestMapper.MapDetailsRequest(merchantId, merchantReferenceNumber, clock);
            return new PaymentResponse { };
        }
    }
}
