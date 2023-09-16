using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickSlot.UserService.Application;
using QuickSlot.UserService.Domain;
using QuickSlot.UserService.Infrastructure;
using System.Reflection;
using System.Text.Json.Serialization;

namespace QuickSlot.UserService.Api
{
    public  class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);
            services.AddDomainServices();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddLogging(config =>
            {
                config.AddConsole();
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
