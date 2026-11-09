using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using CarRental.Application.Features.Auth.DTOs;
using CarRental.Application.Features.Auth.Errors;
using RefreshTokenEntity = 
    CarRental.Domain.Identity.Entities.RefreshTokens.RefreshToken;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CarRental.Application.Common.Security;
using CarRental.Application.Common.Constant;

namespace CarRental.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    ICarRentalDbContext context,
    IOptions<RefreshTokenSettings> options,
    IJwtTokenProvider tokenProvider,
    IRefreshTokenGenerator refreshTokenGenerator,
    IRefreshTokenHasher refreshTokenHasher,
    TimeProvider time) :
    IRequestHandler<RefreshTokenCommand, Result<AuthDto>>
{
    public async Task<Result<AuthDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenHash = refreshTokenHasher.Hash(request.RefreshToken);

        var oldRefreshToken = await context.RefreshTokens.
            FirstOrDefaultAsync(r => r.TokenHash == tokenHash, cancellationToken);
        
        if (oldRefreshToken is null)
            return AuthErrors.InvalidRefreshToken();

        var now = time.GetUtcNow().UtcDateTime;

        if (!oldRefreshToken.IsActive(now))
            return AuthErrors.InvalidRefreshToken();

        var user = await context.Users
            .Include(ur => ur.UserRoles)
            .ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(u => u.UserId == oldRefreshToken.UserId, cancellationToken);

        if (user is null)
            return AuthErrors.InvalidRefreshToken();

        if (!user.IsActive)
            return AuthErrors.AccountInactive();

        if (user.IsLockedOut(now))
            return AuthErrors.AccountLocked();

        var token = tokenProvider.GenerateToken(user);

        var refreshToken = refreshTokenGenerator.GenerateRefreshToken();

        var refreshTokenExpiresAt = now.AddDays(
            options.Value.ExpirationInDays);

        var newRefreshToken = RefreshTokenEntity.Create(
            user.UserId,
            refreshTokenHasher.Hash(refreshToken),
            token.JwtId,
            now,
            refreshTokenExpiresAt,
            request.RevokeByIp
        );

        oldRefreshToken.Revoke(
            now,
            RefreshTokenRevocationReasons.Rotated,
            request.RevokeByIp,
            newRefreshToken.TokenHash
        );

        await context.RefreshTokens.AddAsync(newRefreshToken, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result<AuthDto>.Success(
            new AuthDto(
                token.AccessToken,
                refreshToken,
                token.ExpiresAtUtc));
    }
}