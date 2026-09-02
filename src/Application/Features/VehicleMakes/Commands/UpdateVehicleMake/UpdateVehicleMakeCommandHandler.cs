using CarRental.Application.Features.VehicleMakes.DTOs;
using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using CarRental.Application.Features.VehicleMakes.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CarRental.Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;
using CarRental.Application.Features.VehicleMakes.Mappings;

namespace Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;

public sealed class UpdateVehicleMakeCommandHandler(ICarRentalDbContext context) : IRequestHandler<UpdateVehicleMakeCommand, Result<VehicleMakeDto>>
{
    public async Task<Result<VehicleMakeDto>> Handle(UpdateVehicleMakeCommand request, CancellationToken cancellationToken)
    {
        var vehicleMake = await context.VehicleMakes.
            FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (vehicleMake is null)
            return VehicleMakeErrors.NotFound(request.Id);

        bool nameExists = await context.VehicleMakes.
            AnyAsync(v => v.Name == request.Name &&
                        v.Id != request.Id, cancellationToken);

        if (nameExists)
            return VehicleMakeErrors.NameAlreadyExists;

        vehicleMake.Name = request.Name;

        await context.SaveChangesAsync(cancellationToken);

        return Result<VehicleMakeDto>.Success(vehicleMake.ToDto());
    }
}