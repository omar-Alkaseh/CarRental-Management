using CarRental.Domain.Fleet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Fleet
{
    public class VehicleBodyTypeConfiguration : IEntityTypeConfiguration<VehicleBodyType>
    {
        public void Configure(EntityTypeBuilder<VehicleBodyType> builder)
        {
            builder.ToTable("VehicleBodyTypes", "fleet");

            builder.HasIndex(e => e.Name, "UQ_VehicleBodyTypes_Name").IsUnique();

            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.Description).HasMaxLength(300);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);
        }
    }
}
