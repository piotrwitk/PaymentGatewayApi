using NFluent;
using NUnit.Framework;
using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors.Models;
using System;

namespace PaymentGateway.Tests
{
    public class PaymentProcessorMapperTests
    {
        [Test]
        public void MapsCorrectly_GatewayRequest_To_ProcessorRequest()
        {
            var gatewayRequest = GetTestGatewayRequest();
            var result = PaymentProcessorMapper.MapRequest(gatewayRequest);
            
            Check.That(result).IsNotNull();
            Check.That(result.LongCardNumber).Equals(gatewayRequest.LongNumber);
            Check.That(result.CCV).Equals(gatewayRequest.CVV);
            Check.That(result.GatewayId).Equals(gatewayRequest.GatewayId);
            Check.That(result.NameOnCard).Equals(gatewayRequest.NameOnCard);
            Check.That(result.PaymentAmount).Equals(gatewayRequest.PaymentAmount);
            Check.That(result.PaymentCurrency).Equals(gatewayRequest.PaymentCurrency);            
        }

        [Test]
        public void MapsCorrectly_PaymentProcessorResponse()
        {
            var gatewayRequest = GetTestGatewayRequest();
            var response = new PaymentProcessorResponse
            {
                GatewayId = gatewayRequest.GatewayId,
                FailureReason = "funds not sufficient",
                IsSuccess = false,
                PaymentProcessorReference = Guid.NewGuid().ToString()
            };

            var result = PaymentProcessorMapper.MapResponse(response, gatewayRequest);

            Check.That(response).IsNotNull();
            Check.That(result.TruncatedCardNumber).IsNotEqualTo(gatewayRequest.LongNumber);            
            Check.That(result.GatewayId).Equals(gatewayRequest.GatewayId);
            Check.That(result.PaymentAmount).Equals(gatewayRequest.PaymentAmount);
            Check.That(result.PaymentCurrency).Equals(gatewayRequest.PaymentCurrency);
            Check.That(result.PaymentProcessorId).Equals(response.PaymentProcessorReference);
            Check.That(result.MerchantId).Equals(gatewayRequest.MerchantId);
            Check.That(result.MerchantReferenceNumber).Equals(gatewayRequest.MerchantReferenceNumber);
        }


        private static GatewayPaymentRequest GetTestGatewayRequest()
        {
            return new GatewayPaymentRequest
            {
                PaymentAmount = 123.45d,
                PaymentCurrency = "USD",
                CVV = 512,
                GatewayId = Guid.NewGuid().ToString(),
                LongNumber = "1234567890",
                MerchantId = "merchant_id",
                MerchantReferenceNumber = "merchant_ref",
                NameOnCard = "John Doe",
                TimeStamp = DateTimeOffset.UtcNow,
                ValidFrom = null,
                ValidTo = "12/2029"
            };
        }
    }
}
