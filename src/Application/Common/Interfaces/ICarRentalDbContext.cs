using CarRental.Domain.Fleet.Entities;
using CarRental.Domain.Identity.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Common.Interfaces
{
    public interface ICarRentalDbContext
    {
        DbSet<VehicleModel> VehicleModels { get; }

        DbSet<VehicleMake> VehicleMakes { get; }

        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
