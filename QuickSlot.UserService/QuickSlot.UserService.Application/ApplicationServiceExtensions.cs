using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace QuickSlot.UserService.Application
{
    // Located in Application layer
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }

}
