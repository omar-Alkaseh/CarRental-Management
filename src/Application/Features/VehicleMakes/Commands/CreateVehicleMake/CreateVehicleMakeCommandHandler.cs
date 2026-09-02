using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using CarRental.Application.Features.VehicleMakes.Errors;
using CarRental.Domain.Fleet.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMake
{
    public sealed class CreateVehicleMakeCommandHandler(ICarRentalDbContext context) : IRequestHandler<CreateVehicleMakeCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateVehicleMakeCommand request, CancellationToken cancellationToken)
        {
            bool exists = await context.VehicleMakes
                .AnyAsync(
                    v => v.Name == request.Name,
                    cancellationToken);

            if (exists)
                return VehicleMakeErrors.NameAlreadyExists;

            var vehicleMake = new VehicleMake
            {
                Name = request.Name
            };

            await context.VehicleMakes.AddAsync(vehicleMake, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(vehicleMake.VehicleMakeId);
        }
    }
}
