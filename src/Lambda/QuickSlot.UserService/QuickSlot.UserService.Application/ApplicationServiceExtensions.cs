using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using QuickSlot.UserService.Application.Validation;
using QuickSlot.UserService.Application.Behavior;
using FluentValidation;

namespace QuickSlot.UserService.Application
{
    // Located in Application layer
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }

}
