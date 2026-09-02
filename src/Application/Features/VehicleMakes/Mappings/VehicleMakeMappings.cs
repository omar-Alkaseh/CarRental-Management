using CarRental.Application.Features.VehicleMakes.DTOs;
using CarRental.Domain.Fleet.Entities;

namespace CarRental.Application.Features.VehicleMakes.Mappings;

public static class VehicleMakeMappings
{
    public static VehicleMakeDto ToDto(this VehicleMake vehicleMake) =>
        new VehicleMakeDto
        (
            vehicleMake.Id,
            vehicleMake.Name,
            vehicleMake.IsActive
        );

    public static IEnumerable<VehicleMakeDto> ToListDto(this IEnumerable<VehicleMake> vehicleMakes) =>
        vehicleMakes.Select(v => v.ToDto()).ToList();
}