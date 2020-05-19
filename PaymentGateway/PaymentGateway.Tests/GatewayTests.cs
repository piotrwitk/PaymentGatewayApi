using Moq;
using NFluent;
using NUnit.Framework;
using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.PaymentProcessors.Models;
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
        public void WhenHandlingRequestGatewayShouldCallPaymentProcessor()
        {            
            paymentProcessor.Setup(p => p.HandlePaymentRequest(It.IsAny<PaymentProcessorRequest>()))
                .Returns(Task.FromResult(new PaymentProcessorResponse { IsSuccess = true }));

            var gatewayResponse = gateway.HandleIncomingPaymentRequest(new GatewayPaymentRequest { });

            Check.That(gatewayResponse).IsNotNull();
            paymentProcessor.Verify(p => p.HandlePaymentRequest(It.IsAny<PaymentProcessorRequest>()), Times.Once);

        }
    }
}