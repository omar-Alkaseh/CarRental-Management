using CarRental.Application.Common.Security;
using CarRental.Domain.Identity.Entities.Users;

namespace CarRental.Application.Common.Interfaces;

public interface IJwtTokenProvider
{
    AccessTokenResult GenerateToken(User user);
}