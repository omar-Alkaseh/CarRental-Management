using CarRental.Application.Common.Security;
using CarRental.Domain.Identity.Entities.Users;

namespace CarRental.Application.Common.Interfaces;

public interface IJwtTokenProvider
{
    TokenResult GenerateToken(User user);
}