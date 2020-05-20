using NFluent;
using NUnit.Framework;
using PaymentGateway.Models;
using PaymentGateway.WebApi.Models.v1;
using System;

namespace PaymentGateway.WebApi.Tests
{
    public class V1ResponseMapper
    {
        [Test]
        public void CanMapGatewayResponseToResponse()
        {
            var source = new GatewayResponse
            {
                MerchantId = "merchantId",
                MerchantReferenceNumber = "refNumber",
                IsSuccess = true,
                FailureReason = "operation successfull",
                GatewayId = "gatewayId",
                PaymentAmount = 123.45d,
                PaymentCurrency = "USD",
                TruncatedCardNumber = "***0404",
                TimeStamp = DateTimeOffset.UtcNow
            };

            var result = ResponseMapper.MapResponse(source);

            Check.That(result.IsSuccess).Equals(source.IsSuccess);
            Check.That(result.FailureReason).Equals(source.FailureReason);
            Check.That(result.MerchantId).Equals(source.MerchantId);
            Check.That(result.MerchantReferenceNumber).Equals(source.MerchantReferenceNumber);
            Check.That(result.PaymentAmount).Equals(source.PaymentAmount);
            Check.That(result.PaymentCurrency).Equals(source.PaymentCurrency);
            Check.That(result.TimeStamp).Equals(source.TimeStamp);
            Check.That(result.TruncatedCardNumber).Equals(source.TruncatedCardNumber);
        }
    }
}
