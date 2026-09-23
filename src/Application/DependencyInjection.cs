using CarRental.Application.Common.Behaviors;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;
    public static class DependencyInjection 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
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
