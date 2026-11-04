namespace Car_Rental_Management.Features.Auth.Requests;

public sealed record LoginRequest(string Email, string Password);