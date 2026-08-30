using CarRental.Domain.Fleet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Fleet
{
    public class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.ToTable("VehicleImages", "fleet");

            builder.HasIndex(e => new { e.VehicleId, e.SortOrder }, "IX_VehicleImages_VehicleId_SortOrder");

            builder.HasIndex(e => e.VehicleId, "UX_VehicleImages_OneMainPerVehicle")
                .IsUnique()
                .HasFilter("([IsMain]=(1))");

            builder.Property(e => e.AltText).HasMaxLength(200);
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.ImageUrl).HasMaxLength(1000);

            builder.HasOne(d => d.Vehicle).WithMany(p => p.VehicleImages)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VehicleImages_Vehicles");
        }
    }
}
