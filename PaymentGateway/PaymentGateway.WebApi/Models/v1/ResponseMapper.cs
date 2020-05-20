using PaymentGateway.Models;

namespace PaymentGateway.WebApi.Models.v1
{
    public static class ResponseMapper
    {
        public static PaymentResponse MapResponse(GatewayResponse response)
        {
            return new PaymentResponse 
            {
                MerchantId = response.MerchantId,
                MerchantReferenceNumber = response.MerchantReferenceNumber,
                IsSuccess = response.IsSuccess,
                FailureReason = response.FailureReason,
                PaymentAmount = response.PaymentAmount,
                PaymentCurrency = response.PaymentCurrency,
                TimeStamp = response.TimeStamp,
                TruncatedCardNumber = response.TruncatedCardNumber
            };
        }
    }
}
