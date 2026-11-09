using CarRental.Application.Common.Interfaces;
using CarRental.Domain.Identity.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Auth;

public sealed class PasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<object> _hasher = new();
    public string HashPassword(string password)
    {
        return _hasher.HashPassword(new(), password);
    }

    public bool VerifyPassword(string hashPassword, string password)
    {
        var result = _hasher.VerifyHashedPassword(
            new(),
            hashPassword,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}