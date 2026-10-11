using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using CarRental.Application.Features.VehicleMakes.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.VehicleMakes.Commands.DeleteVehicleMake
{
    public sealed class DeleteVehicleMakeCommandHandler(ICarRentalDbContext context) : IRequestHandler<DeleteVehicleMakeCommand, Result>
    {
        public async Task<Result> Handle(DeleteVehicleMakeCommand request, CancellationToken cancellationToken)
        {
            var vehicleMake = await context.VehicleMakes
                .SingleOrDefaultAsync(
                v => v.Id == request.Id,
                cancellationToken);

            if (vehicleMake is null)
                return VehicleMakeErrors.NotFound(request.Id);

            vehicleMake.Deactivate();

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
