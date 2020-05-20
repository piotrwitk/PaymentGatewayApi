using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NFluent;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.WebApi.Filters;
using PaymentGateway.WebApi.Utils;

namespace PaymentGateway.WebApi.Tests
{
    public class PaymentControllerTests
    {
        private HttpClient client;
        private TestServer server;

        [SetUp]
        public void SetUp()
        {

            var hostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseTestServer()
                .UseStartup<Startup>()
                .ConfigureServices(s => 
                {
                    s.AddSingleton<IGatewayClock, GatewayClock>();
                    s.AddSingleton<ISystemClock, SystemClock>();
                    s.AddSingleton<IPaymentProcessor, SimulatedPaymentProcessor>();
                })
                ;


            server = new TestServer(hostBuilder);
            var test = server.Services.GetRequiredService<IGatewayClock>();

            client = server.CreateClient();
        }

        [Test]
        public async Task Test()
        {
            var result = await client.GetAsync("api/v1/payment/ /123");
            Check.That(result.StatusCode).Equals(HttpStatusCode.BadRequest);
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
            server.Dispose();
        }
    }
}
