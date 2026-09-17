using CarRental.Application.Common.Results;
using MediatR;

namespace CarRental.Application.Features.VehicleMakes.Commands.DeleteVehicleMake
{
    public sealed record DeleteVehicleMakeCommand(int Id) : IRequest<Result>;
}
