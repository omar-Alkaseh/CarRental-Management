using CarRental.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Application
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(
                    typeof(DependencyInjection).Assembly);

                config.AddOpenBehavior(
                    typeof(ValidationBehavior<, >));

            });

            services.AddValidatorsFromAssembly(
                typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
