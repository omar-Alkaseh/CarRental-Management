using CarRental.Domain.Fleet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Fleet
{
    public class VehicleCategoryConfiguration : IEntityTypeConfiguration<VehicleCategory>
    {
        public void Configure(EntityTypeBuilder<VehicleCategory> builder)
        {
            builder.ToTable("VehicleCategories", "fleet");

            builder.HasIndex(e => e.Name, "UQ_VehicleCategories_Name").IsUnique();

            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.DailyRate).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.LateFeePerHour).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.MinimumDriverAge).HasDefaultValue((byte)18);
            builder.Property(e => e.Name).HasMaxLength(80);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.SecurityDeposit).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);
            builder.Property(e => e.WeeklyRate).HasColumnType("decimal(18, 2)");
        }
    }
}
