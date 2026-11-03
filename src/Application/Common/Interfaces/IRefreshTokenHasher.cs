namespace CarRental.Application.Common.Interfaces;

public interface IRefreshTokenHasher
{
    string Hash(string token);
}