using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.DAL;
using PaymentGateway.PaymentProcessors;
using PaymentGateway.WebApi.Filters;
using PaymentGateway.WebApi.Utils;

namespace PaymentGateway.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks().AddCheck<ApiHealthCheck>("api_health_check");
            services.AddSingleton<IPaymentProcessor, SimulatedPaymentProcessor>();
            services.AddSingleton<IGateway, Gateway>();
            services.AddSingleton<IGatewayClock, GatewayClock>();
            services.AddSingleton<IRepository, InMemoryRepository>();
            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
