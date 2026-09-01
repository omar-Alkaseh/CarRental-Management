using CarRental.Application.Common.Results;
using MediatR;

namespace CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMake
{
    public sealed record CreateVehicleMakeCommand(string Name) : IRequest<Result<int>>;
}