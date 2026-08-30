using CarRental.Domain.Rentals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Rentals
{
    public class VehicleInspectionConfiguration : IEntityTypeConfiguration<VehicleInspection>
    {
        public void Configure(EntityTypeBuilder<VehicleInspection> builder)
        {
            builder.ToTable("VehicleInspections", "rental");

            builder.HasIndex(e => new { e.RentalAgreementId, e.InspectionType }, "UQ_VehicleInspections_Rental_Type").IsUnique();

            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.FuelLevel).HasColumnType("decimal(5, 2)");
            builder.Property(e => e.InspectedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.Notes).HasMaxLength(1000);

            builder.HasOne(d => d.Employee).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_VehicleInspections_Employees");

            builder.HasOne(d => d.RentalAgreement).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.RentalAgreementId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_VehicleInspections_RentalAgreements");

            builder.HasOne(d => d.Vehicle).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_VehicleInspections_Vehicles");
        }
    }
}
