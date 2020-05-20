using Moq;
using NFluent;
using NUnit.Framework;
using PaymentGateway.WebApi.Models.v1;
using PaymentGateway.WebApi.Utils;
using System;

namespace PaymentGateway.WebApi.Tests
{
    public class V1RequestMapperTests
    {
        private Mock<IGatewayClock> clock;

        [SetUp]
        public void SetUp()
        {
            clock = new Mock<IGatewayClock>();
        }

        [Test]
        public void MappingDetails_WhenInputCorrect()
        {
            var merchantId = "merchantId";
            var merchantRef = "merchantRef";
            var timeStamp = DateTimeOffset.UtcNow;
            clock.Setup(c => c.GetCurrentUtcTimestamp()).Returns(timeStamp);
            
            var response = RequestMapper.MapDetailsRequest(merchantId, merchantRef, clock.Object);
            Check.That(response).IsNotNull();
            Check.That(response.MerchantId).Equals(merchantId);
            Check.That(response.MerchantReferenceNumber).Equals(merchantRef);
            Check.That(response.TimeStamp).Equals(timeStamp);
        }

        
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("","valid_ref")]
        [TestCase(null, "valid_ref")]
        [TestCase("valid_id", null)]
        [TestCase("valid_id", "")]
        public void MappingDetails_WhenMissingInput_ShouldThrowBadArgument(string merchantId, string merchantRef)
        {
            Check.ThatCode(() => RequestMapper.MapDetailsRequest(merchantId, merchantRef, clock.Object))
                .Throws<ArgumentException>();
        }
    }
}