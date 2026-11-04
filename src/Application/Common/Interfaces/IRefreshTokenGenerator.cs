namespace CarRental.Application.Common.Interfaces;

public interface IRefreshTokenGenerator
{
    string GenerateRefreshToken();
}