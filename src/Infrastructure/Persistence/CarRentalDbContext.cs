using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Billing.Entities;
using CarRental.Domain.Core.Entities;
using CarRental.Domain.Fleet.Entities;
using CarRental.Domain.Identity.Entities;
using CarRental.Domain.Operations.Entities;
using CarRental.Domain.Rentals.Entities;
using CarRental.Application.Common.Interfaces;

namespace CarRental.Infrastructure.Data;

public class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) 
    : DbContext(options), ICarRentalDbContext
{

    public DbSet<AuditLog> AuditLogs { get; set; }

    public DbSet<Branch> Branches { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<DamageImage> DamageImages { get; set; }

    public DbSet<DamageReport> DamageReports { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Extra> Extras { get; set; }

    public DbSet<Feature> Features { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<RentalAgreement> RentalAgreements { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<ReservationExtra> ReservationExtras { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<VehicleBodyType> VehicleBodyTypes { get; set; }

    public DbSet<VehicleCategory> VehicleCategories { get; set; }

    public DbSet<VehicleImage> VehicleImages { get; set; }

    public DbSet<VehicleInspection> VehicleInspections { get; set; }

    public DbSet<VehicleMake> VehicleMakes { get; set; }

    public DbSet<VehicleModel> VehicleModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarRentalDbContext).Assembly);
    }

}
