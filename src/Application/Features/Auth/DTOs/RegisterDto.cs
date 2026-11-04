namespace CarRental.Application.Features.Auth.DTOs;

public sealed record RegisterDto(    
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password);