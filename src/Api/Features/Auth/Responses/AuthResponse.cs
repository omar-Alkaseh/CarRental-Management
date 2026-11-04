namespace Car_Rental_Management.Features.Auth.Responses;

public sealed record AuthResponse(string AccessToken, string RefreshToken, DateTime ExpiresAtUtc);