namespace CarRental.Application.Features.Auth.DTOs;

public sealed record AuthDto(string AccessToken, string RefreshToken, DateTime ExpiresAtUtc);