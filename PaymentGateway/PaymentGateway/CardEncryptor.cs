using PaymentGateway.Models;
using System;

namespace PaymentGateway
{
    /// <summary>
    /// Basic implementation that currently destroys the LongNumber and only stores last 4 digits of it
    /// In real life this should encrypt LongNumber (and possibly other card data) using symmetric encryption
    /// Also rotation of the keys used for encryption / decription should be taken into an account
    /// </summary>
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
