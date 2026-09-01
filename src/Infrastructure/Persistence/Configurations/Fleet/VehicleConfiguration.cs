using CarRental.Domain.Billing.Enums;
using CarRental.Domain.Fleet.Entities;
using CarRental.Domain.Fleet.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Fleet
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles", "fleet");

            builder.HasIndex(e => new { e.BranchId, e.VehicleCategoryId, e.OperationalStatus, e.IsActive }, "IX_Vehicles_AvailabilitySearch");

            builder.HasIndex(e => e.LicensePlate, "UQ_Vehicles_LicensePlate").IsUnique();

            builder.HasIndex(e => e.Vin, "UQ_Vehicles_Vin").IsUnique();

            builder.Property(e => e.Color).HasMaxLength(40);
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.CurrentFuelLevel)
                .HasDefaultValue(100m)
                .HasColumnType("decimal(5, 2)");
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.LicensePlate).HasMaxLength(20);
            builder.Property(e => e.OperationalStatus).HasDefaultValue(VehicleOperationalStatus.Available).HasSentinel(VehicleOperationalStatus.Unspecified);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);
            builder.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false);

            builder.HasOne(d => d.Branch).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Vehicles_Branches");

            builder.HasOne(d => d.VehicleCategory).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Vehicles_VehicleCategories");

            builder.HasOne(d => d.VehicleModel).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleModelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Vehicles_VehicleModels");

            builder.HasMany(d => d.Features).WithMany(p => p.Vehicles)
                .UsingEntity<Dictionary<string, object>>(
                    "VehicleFeature",
                    r => r.HasOne<Feature>().WithMany()
                        .HasForeignKey("FeatureId")
                        .HasConstraintName("FK_VehicleFeatures_Features"),
                    l => l.HasOne<Vehicle>().WithMany()
                        .HasForeignKey("VehicleId")
                        .HasConstraintName("FK_VehicleFeatures_Vehicles"),
                    j =>
                    {
                        j.HasKey("VehicleId", "FeatureId");
                        j.ToTable("VehicleFeatures", "fleet");
                    });
        }
    }
}
