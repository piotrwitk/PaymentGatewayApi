using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.WebApi.Utils;
using Moq;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.WebApi.Filters;
using System.Threading.Tasks;
using PaymentGateway.WebApi.Models.v1;
using Newtonsoft.Json;
using System.Text;
using NFluent;
using System.Net;
using PaymentGateway.PaymentProcessors.Models;

namespace PaymentGateway.WebApi.Tests
{
    public class IntegrationWithPaymentProviderTests
    {
        private HttpClient client;
        private TestServer server;
        private Mock<IGatewayClock> mockedClock = new Mock<IGatewayClock>();
        private Mock<IPaymentProcessor> mockedPaymentProcessor = new Mock<IPaymentProcessor>();

        [SetUp]
        public void SetUp()
        {            
            var hostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseTestServer()
                .UseStartup<TestStartup>()
                .ConfigureServices(s =>
                {
                    s.AddControllers();
                    s.AddHealthChecks().AddCheck<ApiHealthCheck>("api_health_check");
                    s.AddSingleton<IGateway, Gateway>();
                    s.AddSingleton<IGatewayClock>(mockedClock.Object);
                    s.AddSingleton<IPaymentProcessor>(mockedPaymentProcessor.Object);
                    s.AddMvc(options =>
                    {
                        options.Filters.Add(new ApiExceptionFilter());
                    });
                });

            server = new TestServer(hostBuilder);
            client = server.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
            server.Dispose();
        }

        [TestCase(true, "")]
        [TestCase(false, "failure message")]
        public async Task ReturnsSuccess_WhenPaymentProcessorReturnsSuccess(bool expectedReturn, string expectedFailure)
        {
            var merchantRef = "merchant_ref";

            mockedPaymentProcessor.Setup(p => p.HandlePaymentRequest(It.IsAny<PaymentProcessorRequest>()))
                .ReturnsAsync(new PaymentProcessorResponse 
                { 
                    IsSuccess = expectedReturn,
                    FailureReason = expectedFailure
                });

            var paymentRequest = new PaymentRequest
            {
                MerchantReferenceNumber = merchantRef
            };

            var content = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/v1/payment/123", content);

            Check.That(response.StatusCode).Equals(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaymentResponse>(responseContent);

            Check.That(result.IsSuccess).Equals(expectedReturn);
            Check.That(result.MerchantReferenceNumber).Equals(merchantRef);
            Check.That(result.FailureReason).Equals(expectedFailure);
        }
    }
}
