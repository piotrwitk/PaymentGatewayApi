using PaymentGateway.Models;
using System;

namespace PaymentGateway
{
    public class CardEncryptor
    {
        public static GatewayPaymentRequest EncryptCardData(GatewayPaymentRequest originalRequest)
        {
            var encrypted = new GatewayPaymentRequest
            {
                CVV = originalRequest.CVV,
                LongNumber = "**********",
                MerchantId = originalRequest.MerchantId,
                MerchantReferenceNumber = originalRequest.MerchantReferenceNumber,
                NameOnCard = originalRequest.NameOnCard,
                PaymentAmount = originalRequest.PaymentAmount,
                PaymentCurrency = originalRequest.PaymentCurrency,
                TimeStamp = originalRequest.TimeStamp,
                TruncatedNumber = originalRequest.LongNumber.Substring(Math.Max(0, originalRequest.LongNumber.Length - 4)),
                ValidFrom = originalRequest.ValidFrom,
                ValidTo = originalRequest.ValidTo
            };

            return encrypted;
        }
    }
}
