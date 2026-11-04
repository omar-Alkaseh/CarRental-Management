using System.Security.Cryptography;
using System.Text;
using CarRental.Application.Common.Interfaces;

namespace CarRental.Infrastructure.Auth;

public sealed class RefreshTokenHasher : IRefreshTokenHasher
{
    public string Hash(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));

        return Convert.ToHexString(bytes);
    }
}