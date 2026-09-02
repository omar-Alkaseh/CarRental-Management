using CarRental.Application.Common.Results;
using MediatR;

namespace CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMakes
{
    public sealed record CreateVehicleMakeCommand(string Name) : IRequest<Result<int>>;
}