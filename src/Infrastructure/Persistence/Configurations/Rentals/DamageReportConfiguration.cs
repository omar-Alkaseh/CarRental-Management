using CarRental.Domain.Rentals.Entities;
using CarRental.Domain.Rentals.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Rentals
{
    public class DamageReportConfiguration : IEntityTypeConfiguration<DamageReport>
    {
        public void Configure(EntityTypeBuilder<DamageReport> builder)
        {
            builder.ToTable("DamageReports", "rental");

            builder.Property(e => e.ActualRepairCost).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.Description).HasMaxLength(1000);
            builder.Property(e => e.EstimatedRepairCost).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.LocationOnVehicle).HasMaxLength(100);
            builder.Property(e => e.ResolvedAtUtc).HasPrecision(0);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.Status).HasDefaultValue(DamageReportStatus.Reported).HasSentinel(DamageReportStatus.Unspecified);
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);

            builder.HasOne(d => d.VehicleInspection).WithMany(p => p.DamageReports)
                .HasForeignKey(d => d.VehicleInspectionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DamageReports_VehicleInspections");
        }
    }
}
