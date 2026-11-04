namespace Car_Rental_Management.Features.Auth.Requests;

public sealed record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber);