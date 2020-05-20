using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.WebApi.Utils;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.WebApi.Filters;
using System.Threading.Tasks;
using PaymentGateway.WebApi.Models.v1;
using Newtonsoft.Json;
using System.Text;
using NFluent;
using System.Net;
using PaymentGateway.DAL;
using System;

namespace PaymentGateway.WebApi.Tests
{
    public class IntegrationWithRepository
    {
        private HttpClient client;
        private TestServer server;

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
                    s.AddSingleton<IGatewayClock, GatewayClock>();
                    s.AddSingleton<IPaymentProcessor, SimulatedPaymentProcessor>();
                    s.AddSingleton<IRepository, InMemoryRepository>();
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
        public async Task MerchantCanProcessNewRequestAndRetrieveDetails()
        {
            PaymentRequest request = GetTestRequest();
            var merchantId = "black_sheep_coffee";

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/v1/payment/{merchantId}", content);
            Check.That(response.StatusCode).Equals(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseDetails = JsonConvert.DeserializeObject<PaymentResponse>(responseContent);
            Check.That(responseDetails).IsNotNull();

            var detailsResponse = await client.GetAsync($"api/v1/payment/{merchantId}/{request.MerchantReferenceNumber}");
            Check.That(detailsResponse.StatusCode).Equals(HttpStatusCode.OK);

            var detailsContent = await detailsResponse.Content.ReadAsStringAsync();
            var details = JsonConvert.DeserializeObject<PaymentResponse>(detailsContent);
            Check.That(details).IsNotNull();

            Check.That(details.IsSuccess).IsEqualTo(responseDetails.IsSuccess);
            Check.That(details.PaymentAmount).IsEqualTo(responseDetails.PaymentAmount);
        }

        [Test]
        public async Task NotFoudnShouldBeReturnedIfDetailsCantBeFound()
        {
            PaymentRequest request = GetTestRequest();
            var merchantId = "black_sheep_coffee";

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/v1/payment/{merchantId}", content);
            Check.That(response.StatusCode).Equals(HttpStatusCode.OK);

            var detailsResponse = await client.GetAsync($"api/v1/payment/{merchantId}/invalid_merchant_ref_number");
            Check.That(detailsResponse.StatusCode).Equals(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task NotFoundShouldBeReturnedIfNoMerchantIdCanBeFound()
        {
            PaymentRequest request = GetTestRequest();
            var merchantId = "black_sheep_coffee";

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/v1/payment/{merchantId}", content);
            Check.That(response.StatusCode).Equals(HttpStatusCode.OK);

            var detailsResponse = await client.GetAsync($"api/v1/payment/non_existing_merchant/{request.MerchantReferenceNumber}");
            Check.That(detailsResponse.StatusCode).Equals(HttpStatusCode.NotFound);

        }

        private static PaymentRequest GetTestRequest()
        {
            return new PaymentRequest
            {
                MerchantReferenceNumber = Guid.NewGuid().ToString(),
                Amount = 123.45d,
                Currency = "GBP",
                CVV = 100,
                LongCardNumber = "1212121212121212",
                NameOnCard = "Mr John Doe",
                ValidFrom = null,
                ValidTo = "12/2030"
            };
        }

    }
}
