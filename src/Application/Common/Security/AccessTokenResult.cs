namespace CarRental.Application.Common.Security;

public sealed record AccessTokenResult(
    string AccessToken,
    DateTime ExpiresAtUtc,
    Guid JwtId
);