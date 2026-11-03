using CarRental.Application.Common.Interfaces;
using CarRental.Domain.Identity.Entities.Users;
using CarRental.Infrastructure.Auth;
using CarRental.Infrastructure.Auth.Settings;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Persistence.Options;
using Microsoft.AspNetCore.Identity;
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

            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
            
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();

            services.AddScoped<IRefreshTokenHasher, RefreshTokenHasher>();

            return services;
        }
    }
