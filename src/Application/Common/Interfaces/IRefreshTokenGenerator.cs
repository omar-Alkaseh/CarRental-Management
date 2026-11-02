namespace CarRental.Application.Common.Interfaces;

public interface IRefreshTokenGenerator
{
    string GenerateRefreshToken();
    string HashRefreshToken(string token);
}