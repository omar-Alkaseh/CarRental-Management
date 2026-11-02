using System.Security.Cryptography;
using System.Text;
using CarRental.Application.Common.Interfaces;

namespace CarRental.Infrastructure.Auth;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }

    public string HashRefreshToken(string token)
    {
        var bytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = SHA256.HashData(bytes);

        return Convert.ToBase64String(hashBytes);
    }
}