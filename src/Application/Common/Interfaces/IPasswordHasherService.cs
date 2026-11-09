namespace CarRental.Application.Common.Interfaces;

public interface IPasswordHasherService
{
    string HashPassword(string password);

    bool VerifyPassword(string hashPassword, string password);
}