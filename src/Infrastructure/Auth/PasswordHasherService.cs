using CarRental.Application.Common.Interfaces;
using CarRental.Domain.Identity.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Auth;

public sealed class PasswordHasherService(IPasswordHasher<User> passwordHasher) :
    IPasswordHasherService
{
    public string HashPassword(User user, string password)
    {
        return passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string password)
    {
        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}