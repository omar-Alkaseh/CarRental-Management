using CarRental.Application.Common.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<DatabaseOptions>()
                .Bind(configuration.GetSection(DatabaseOptions.SectionName))
                .Validate(
                    options => !string.IsNullOrWhiteSpace(
                        options.CarRentalDatabase),
                    "Car rental database connection string is required.")
                .ValidateOnStart();

            services.AddDbContext<CarRentalDbContext>((serviceProvider, options) =>
            {
                var databaseOptions = serviceProvider
                    .GetRequiredService<IOptions<DatabaseOptions>>()
                    .Value;

                options.UseSqlServer(databaseOptions.CarRentalDatabase);
            });

            services.AddScoped<ICarRentalDbContext, CarRentalDbContext>();

            return services;
        }
    }
