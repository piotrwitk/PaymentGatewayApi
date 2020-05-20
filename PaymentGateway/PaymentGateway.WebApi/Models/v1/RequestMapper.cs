using PaymentGateway.Models;
using PaymentGateway.WebApi.Utils;
using System;

namespace PaymentGateway.WebApi.Models.v1
{
    public static class RequestMapper
    {
        public static GatewayDetailsRequest MapDetailsRequest(string merchantId, string merchantReferenceNumber, IGatewayClock clock)
        {

            if (string.IsNullOrWhiteSpace(merchantId) || string.IsNullOrWhiteSpace(merchantReferenceNumber))
            {
                throw new ArgumentException("Missing: " + 
                    (string.IsNullOrWhiteSpace(merchantId) ? "merchant id " : "") +
                    (string.IsNullOrWhiteSpace(merchantId) ? "merchant reference number " : ""));
            }

            return new GatewayDetailsRequest
            {
                MerchantId = merchantId,
                MerchantReferenceNumber = merchantReferenceNumber,
                TimeStamp = clock.GetCurrentUtcTimestamp()
            };
        }
    }
}
