using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NFluent;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.WebApi.Utils;
using Moq;
using Newtonsoft.Json;
using PaymentGateway.WebApi.Models.v1;
using System;
using PaymentGateway.Models;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.WebApi.Filters;
using System.Text;

namespace PaymentGateway.WebApi.Tests
{
    public class PaymentControllerTests
    {
        private HttpClient client;
        private TestServer server;
        private Mock<IGateway> mockedGateway;
        private Mock<IGatewayClock> mockedClock;

        [SetUp]
        public void SetUp()
        {
            mockedGateway = new Mock<IGateway>();
            mockedClock = new Mock<IGatewayClock>();

            var hostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseTestServer()
                .UseStartup<TestStartup>()
                .ConfigureServices(s => 
                {
                    s.AddControllers();
                    s.AddHealthChecks().AddCheck<ApiHealthCheck>("api_health_check");
                    s.AddSingleton<IGateway>(mockedGateway.Object);
                    s.AddSingleton<IGatewayClock>(mockedClock.Object);
                    s.AddSingleton<IPaymentProcessor, SimulatedPaymentProcessor>();
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

        [Test]
        public async Task Returns_BadRequest_WhenDetailsRequestMissing()
        {
            var result = await client.GetAsync("api/v1/payment/ /123");
            Check.That(result.StatusCode).Equals(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Return_BadRequest_WhenNoPayloadForPayment()
        {
            var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/v1/payment/123", content);
            Check.That(result.StatusCode).Equals(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Return_BadRequest_WhenNoMerchantForPayment()
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PaymentRequest { }), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/v1/payment/ /", content);
            Check.That(result.StatusCode).Equals(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Returns_Response_WhenGatewayReturnsData()
        {
            var merchantId = "merchantId";
            var merchantRef = "merchantRef";
            var timestamp = DateTimeOffset.UtcNow;
            
            mockedClock.Setup(c => c.GetCurrentUtcTimestamp()).Returns(timestamp);
            mockedGateway.Setup(g => g.HandlePaymentRequest(It.IsAny<GatewayPaymentRequest>()))
                .ReturnsAsync(new GatewayResponse
                {
                    MerchantId = merchantId,
                    MerchantReferenceNumber = merchantRef,
                    IsSuccess = true,
                    TimeStamp = timestamp
                });

            var content = new StringContent(JsonConvert.SerializeObject(new PaymentRequest { }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/v1/payment/123", content);

            Check.That(response.StatusCode).Equals(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaymentResponse>(responseContent);

            Check.That(result).IsNotNull();
            Check.That(result.MerchantId).Equals(merchantId);
            Check.That(result.MerchantReferenceNumber).Equals(merchantRef);
            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.TimeStamp).Equals(timestamp);
        }

        [Test]
        public async Task Returns_DetailsResponse_WhenGatewayReturnsData()
        {
            var merchantId = "merchantId";
            var merchantRef = "merchantRef";
            var timestamp = DateTimeOffset.UtcNow;

            mockedClock.Setup(c => c.GetCurrentUtcTimestamp()).Returns(timestamp);
            mockedGateway.Setup(g => g.HandleDetailsRequest(It.IsAny<GatewayDetailsRequest>()))
                .ReturnsAsync(new GatewayResponse
                {
                    MerchantId = merchantId,
                    MerchantReferenceNumber = merchantRef,
                    IsSuccess = true,
                    TimeStamp = timestamp
                });

            var response = await client.GetAsync($"api/v1/payment/{merchantId}/{merchantRef}");

            Check.That(response.StatusCode).Equals(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaymentResponse>(content);

            Check.That(result).IsNotNull();
            Check.That(result.MerchantId).Equals(merchantId);
            Check.That(result.MerchantReferenceNumber).Equals(merchantRef);
            Check.That(result.IsSuccess).IsTrue();
            Check.That(result.TimeStamp).Equals(timestamp);
        }
    }
}
