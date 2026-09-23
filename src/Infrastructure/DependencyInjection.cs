using CarRental.Application.Common.Interfaces;
using CarRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarRentalDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("CarRentalDatabase")));

            services.AddScoped<ICarRentalDbContext, CarRentalDbContext>();

            return services;
        }
    }
