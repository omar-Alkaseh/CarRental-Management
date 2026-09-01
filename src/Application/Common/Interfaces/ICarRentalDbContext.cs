using CarRental.Domain.Fleet.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Common.Interfaces
{
    public interface ICarRentalDbContext
    {
        DbSet<VehicleModel> VehicleModels { get; }

        DbSet<VehicleMake> VehicleMakes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
