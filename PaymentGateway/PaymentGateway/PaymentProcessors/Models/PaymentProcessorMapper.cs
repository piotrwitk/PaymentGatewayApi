using PaymentGateway.Models;

namespace PaymentGateway.PaymentProcessors.Models
{
    public static class PaymentProcessorMapper
    {
        public static GatewayResponse MapResponse(PaymentProcessorResponse response, GatewayPaymentRequest request)
        {
            return new GatewayResponse
            {
                MerchantId = request.MerchantId,
                MerchantReferenceNumber = request.MerchantReferenceNumber,
                TimeStamp = request.TimeStamp,
                GatewayId = request.GatewayId,
                IsSuccess = response.IsSuccess,
                FailureReason = response.FailureReason,
                PaymentProcessorId = response.PaymentProcessorReference,
                TruncatedCardNumber = "***",
                PaymentAmount = request.PaymentAmount,
                PaymentCurrency = request.PaymentCurrency
            };
        }


        public static PaymentProcessorRequest MapRequest(GatewayPaymentRequest request)
        {
            return new PaymentProcessorRequest
            {
                CCV = request.CVV,
                GatewayId = request.GatewayId,
                LongCardNumber = request.LongNumber,
                NameOnCard = request.NameOnCard,
                ValidFrom = request.ValidFrom,
                ValidTo = request.ValidTo,
                PaymentAmount = request.PaymentAmount,
                PaymentCurrency = request.PaymentCurrency
            };
        }
    }
}
