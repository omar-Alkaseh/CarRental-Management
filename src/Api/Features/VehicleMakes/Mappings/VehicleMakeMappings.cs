using CarRental.Application.Features.VehicleMakes.DTOs;
using CarRental.Application.Features.VehicleMakes.Responses;

namespace CarRental.Application.Features.VehicleMakes.Mappings;

public static class VehicleMakeMappings
{
    public static VehicleMakeResponse ToResponse(this VehicleMakeDto vehicleMakeDto) =>
        new VehicleMakeResponse
        (
            vehicleMakeDto.Id,
            vehicleMakeDto.Name,
            vehicleMakeDto.IsActive
        );

    public static IEnumerable<VehicleMakeResponse> ToListResponse(this IEnumerable<VehicleMakeDto> vehicleMakeDtos) =>
        vehicleMakeDtos.Select(v => v.ToResponse()).ToList();
}