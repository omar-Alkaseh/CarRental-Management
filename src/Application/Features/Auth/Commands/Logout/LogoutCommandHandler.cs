using CarRental.Application.Common.Constant;
using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandHandler(
    ICarRentalDbContext context,
    IRefreshTokenHasher refreshTokenHasher,
    TimeProvider time
    ) : IRequestHandler<LogoutCommand, Result>
{
    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var tokenHash = refreshTokenHasher.Hash(request.RefreshToken);

        var refreshToken = await context.RefreshTokens
            .FirstOrDefaultAsync(r => r.UserId == request.UserId &&
            r.TokenHash == tokenHash, cancellationToken);

        var nowUtc = time.GetUtcNow().UtcDateTime;

        if (refreshToken is not null && refreshToken.IsActive(nowUtc))
        {
            refreshToken.Revoke(
                nowUtc,
                RefreshTokenRevocationReasons.Logout,
                request.RevokeByIP);

            await context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}