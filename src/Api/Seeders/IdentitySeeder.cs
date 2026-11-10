using CarRental.Application.Common.Interfaces;
using CarRental.Domain.Identity.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Seeders;

public static class IdentitySeeder
{
        private static readonly (
        string FirstName,
        string LastName,
        string Email,
        string Role
    )[] Accounts =
    [
        ("System", "Admin", "Admin@gmail.com", "ADMIN"),
        ("System", "Manager", "Manager@gmail.com", "MANAGER"),
        ("System", "Employee", "Employee@gmail.com", "EMPLOYEE"),
        ("System", "Customer", "Customer@gmail.com", "CUSTOMER")
    ];

    public static async Task SeedAsync(
        WebApplication app,
        CancellationToken cancellationToken = default)
    {
        // Seed demo accounts in Development only
        if (!app.Environment.IsDevelopment())
            return;

        await using var scope = app.Services.CreateAsyncScope();

        var context = scope.ServiceProvider
            .GetRequiredService<ICarRentalDbContext>();

        var passwordHasher = scope.ServiceProvider
            .GetRequiredService<IPasswordHasherService>();

        var timeProvider = scope.ServiceProvider
            .GetRequiredService<TimeProvider>();

        var configuration = scope.ServiceProvider
            .GetRequiredService<IConfiguration>();

        var nowUtc = timeProvider.GetUtcNow().UtcDateTime;

        foreach (var account in Accounts)
        {
            var normalizedEmail = account.Email.ToUpperInvariant();

            // Skip existing users
            var userExists = await context.Users
                .AnyAsync(
                    u => u.NormalizedEmail == normalizedEmail,
                    cancellationToken);

            if (userExists)
                continue;

            // Find role
            var role = await context.Roles
                .SingleOrDefaultAsync(
                    r => r.NormalizedName == account.Role,
                    cancellationToken);

            if (role is null)
                throw new InvalidOperationException(
                    $"Role '{account.Role}' does not exist.");

            // Read password from configuration
            var password = configuration[
                $"SeedUsers:{account.Role}:Password"];

            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException(
                    $"Missing password for {account.Role}.");

            // Hash password
            var passwordHash = passwordHasher
                .HashPassword(password);

            // Create user
            var user = User.Create(
                account.FirstName,
                account.LastName,
                account.Email,
                passwordHash,
                nowUtc);

            // Assign role
            user.AssignRole(role.RoleId, nowUtc);

            context.Users.Add(user);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}