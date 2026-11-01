using Car_Rental_Management.Features.Auth.Responses;
using CarRental.Application.Features.Auth.DTOs;

namespace Car_Rental_Management.Features.Auth.Mappings;

public static class AuthMappings
{
    public static AuthResponse ToResponse(this AuthDto authDto) =>
        new(authDto.AccessToken, authDto.RefreshToken, authDto.ExpiresAtUtc);

    public static IEnumerable<AuthResponse> ToResponses(this IEnumerable<AuthDto> authDtos) =>
        authDtos.Select(a => a.ToResponse());
}