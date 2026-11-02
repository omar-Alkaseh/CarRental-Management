namespace CarRental.Application.Common.Security;

public sealed record TokenResult(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAtUtc,
    Guid JwtId
);