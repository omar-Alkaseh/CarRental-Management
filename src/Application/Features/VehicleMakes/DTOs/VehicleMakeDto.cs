namespace CarRental.Application.Features.VehicleMakes.DTOs;

public sealed record VehicleMakeDto(
    int Id, 
    string Name,
    bool IsActive
);