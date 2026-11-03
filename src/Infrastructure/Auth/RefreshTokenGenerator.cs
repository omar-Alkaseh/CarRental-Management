using System.Security.Cryptography;
using CarRental.Application.Common.Interfaces;

namespace CarRental.Infrastructure.Auth;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }
}