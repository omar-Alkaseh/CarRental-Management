using CarRental.Domain.Identity.Entities.Users;

namespace CarRental.Application.Common.Interfaces;

public interface IPasswordHasherService
{
    string HashPassword(User user, string password);

    bool VerifyPassword(
        User user,
        string password);
}