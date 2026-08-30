using CarRental.Domain.Operations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Operations
{
    public class MaintenaceRecordConfiguration : IEntityTypeConfiguration<MaintenanceRecord>
    {
        public void Configure(EntityTypeBuilder<MaintenanceRecord> builder)
        {
            builder.ToTable("MaintenanceRecords", "ops");

            builder.HasIndex(e => new { e.VehicleId, e.Status, e.ScheduledStartAtUtc, e.ExpectedEndAtUtc }, "IX_MaintenanceRecords_VehicleId_Status");

            builder.Property(e => e.CompletedAtUtc).HasPrecision(0);
            builder.Property(e => e.CostAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.Description).HasMaxLength(1000);
            builder.Property(e => e.ExpectedEndAtUtc).HasPrecision(0);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.ScheduledStartAtUtc).HasPrecision(0);
            builder.Property(e => e.ServiceProvider).HasMaxLength(200);
            builder.Property(e => e.StartedAtUtc).HasPrecision(0);
            builder.Property(e => e.Status).HasDefaultValue((byte)1);
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);

            builder.HasOne(d => d.CreatedByEmployee).WithMany(p => p.MaintenanceRecords)
                .HasForeignKey(d => d.CreatedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MaintenanceRecords_Employees");

            builder.HasOne(d => d.Vehicle).WithMany(p => p.MaintenanceRecords)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MaintenanceRecords_Vehicles");
        }
    }
}
