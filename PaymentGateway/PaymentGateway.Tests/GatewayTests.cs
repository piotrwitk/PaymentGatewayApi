using Moq;
using NFluent;
using NUnit.Framework;
using PaymentGateway.DAL;
using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.PaymentProcessors.Models;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Tests
{
    public class PaymentControllerTests
    {
        private Mock<IPaymentProcessor> paymentProcessor;
        private IRepository repository;
        private Gateway gateway;

        [SetUp]
        public void Setup()
        {
            paymentProcessor = new Mock<IPaymentProcessor>();
            repository = new InMemoryRepository();
            gateway = new Gateway(paymentProcessor.Object, repository);
        }

        [Test]
        public async Task WhenHandlingPaymentRequestGatewayShouldCallPaymentProcessor()
        {
            var request = new GatewayPaymentRequest
            {
                MerchantId = Guid.NewGuid().ToString(),
                MerchantReferenceNumber = Guid.NewGuid().ToString()
            };

            paymentProcessor.Setup(p => p.HandlePaymentRequest(It.IsAny<PaymentProcessorRequest>()))
                .Returns(Task.FromResult(new PaymentProcessorResponse { IsSuccess = true }));

            var gatewayResponse = await gateway.HandlePaymentRequest(request);

            Check.That(gatewayResponse).IsNotNull();
            paymentProcessor.Verify(p => p.HandlePaymentRequest(It.IsAny<PaymentProcessorRequest>()), Times.Once);

            var storedRequest = await repository.RetrieveRequest(gatewayResponse.GatewayId);
            Check.That(storedRequest).IsNotNull();
            Check.That(storedRequest.MerchantId).Equals(request.MerchantId);
            Check.That(storedRequest.MerchantReferenceNumber).Equals(request.MerchantReferenceNumber);

            var responseDetails = await repository.RetrieveDetails(request.MerchantId, request.MerchantReferenceNumber);
            Check.That(responseDetails).IsNotNull();
            Check.That(responseDetails.MerchantId).Equals(request.MerchantId);
            Check.That(responseDetails.MerchantReferenceNumber).Equals(request.MerchantReferenceNumber);
        }
    }
}