using Moq;
using NFluent;
using NUnit.Framework;
using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using System.Threading.Tasks;

namespace PaymentGateway.Tests
{
    public class Tests
    {
        private Mock<IPaymentProcessor> paymentProcessor;
        private Gateway gateway;

        [SetUp]
        public void Setup()
        {
            paymentProcessor = new Mock<IPaymentProcessor>();
            gateway = new Gateway(paymentProcessor.Object);
        }

        [Test]
        public void WhenHandlingPaymentRequestGatewayShouldCallPaymentProcessor()
        {
            var request = new GatewayPaymentRequest
            {
                GatewayId = "gatewayid"               
            };

            paymentProcessor.Setup(p => p.HandlePaymentRequest(request))
                .Returns(Task.FromResult(new GatewayPaymentResponse { IsSuccess = true }));

            var gatewayResponse = gateway.HandlePaymentRequest(request);

            Check.That(gatewayResponse).IsNotNull();
            paymentProcessor.Verify(p => p.HandlePaymentRequest(request), Times.Once);

        }
    }
}