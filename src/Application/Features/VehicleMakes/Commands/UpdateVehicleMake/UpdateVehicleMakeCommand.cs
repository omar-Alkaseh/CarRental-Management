using CarRental.Application.Features.VehicleMakes.DTOs;
using CarRental.Application.Common.Results;
using MediatR;

namespace CarRental.Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;

public sealed record UpdateVehicleMakeCommand(int Id, string Name) : IRequest<Result<VehicleMakeDto>>;