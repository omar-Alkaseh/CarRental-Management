using CarRental.Application.Common.Interfaces;
using CarRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarRentalDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("CarRentalDatabase")));

            services.AddScoped<ICarRentalDbContext, CarRentalDbContext>();

            return services;
        }
    }
}
