using NFluent;
using NUnit.Framework;
using PaymentGateway.Models;

namespace PaymentGateway.Tests
{
    public class CardEncryptorTests
    {
        [Test]
        public void WillEncryptLongNumberAndProduceTruncatedOne()
        {
            var request = new GatewayPaymentRequest
            {
                LongNumber = "1111222233334444"
            };

            var encrypted = CardEncryptor.EncryptCardData(request);
            Check.That(encrypted.TruncatedNumber).Equals("4444");
            Check.That(encrypted.LongNumber).IsNotEqualTo(request.LongNumber);
        }
    }
}
