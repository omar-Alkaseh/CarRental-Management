using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using CarRental.Application.Common.Security;
using CarRental.Application.Features.Auth.DTOs;
using CarRental.Application.Features.Auth.Errors;
using RefreshTokenEntity =
    CarRental.Domain.Identity.Entities.RefreshTokens.RefreshToken;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarRental.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler(
    ICarRentalDbContext context,
    IOptions<RefreshTokenSettings> options,
    IJwtTokenProvider tokenProvider,
    IRefreshTokenGenerator refreshTokenGenerator,
    IRefreshTokenHasher tokenHasher,
    IPasswordHasherService passwordHasher,
    TimeProvider clock) : IRequestHandler<LoginCommand, Result<AuthDto>>
{
    public async Task<Result<AuthDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().
            ToUpperInvariant();

        var user = await context.Users.Include(u => u.UserRoles).ThenInclude(r => r.Role)
            .SingleOrDefaultAsync(u => u.NormalizedEmail == email, cancellationToken);

        var now = clock.GetUtcNow().UtcDateTime;

        if (user is null)
            return AuthErrors.InvalidCredentials();

        if (!passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
        {
            if (user.IsActive && !user.IsLockedOut(now))
            {
                user.RecordFailedLogin(now, 3, TimeSpan.FromMinutes(15));

                await context.SaveChangesAsync(cancellationToken);
            }

            return AuthErrors.InvalidCredentials();
        }

        if (!user.IsActive)
            return AuthErrors.AccountInactive();

        if (user.IsLockedOut(now))
            return AuthErrors.AccountLocked();

        user.RecordSuccessfulLogin();

        var token = tokenProvider.GenerateToken(user);

        var refreshToken = refreshTokenGenerator.GenerateRefreshToken();

        var refreshTokenExpiresAt = now.AddDays(
            options.Value.ExpirationInDays);

        var newRefreshToken = RefreshTokenEntity.Create(
            user.UserId,
            tokenHasher.Hash(refreshToken),
            token.JwtId,
            now,
            refreshTokenExpiresAt,
            request.RevokeByIp
        );

        await context.RefreshTokens.AddAsync(newRefreshToken, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result<AuthDto>.Success(new AuthDto(
            token.AccessToken,
            refreshToken,
            token.ExpiresAtUtc
        ));
    }
}