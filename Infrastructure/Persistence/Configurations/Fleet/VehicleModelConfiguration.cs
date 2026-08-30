using CarRental.Domain.Fleet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Fleet
{
    public class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
    {
        public void Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            builder.ToTable("VehicleModels", "fleet");

            builder.HasIndex(e => e.VehicleBodyTypeId, "IX_VehicleModels_VehicleBodyTypeId");

            builder.HasIndex(e => new { e.VehicleMakeId, e.Name, e.VehicleBodyTypeId }, "UQ_VehicleModels_Make_Name_BodyType").IsUnique();

            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.Name).HasMaxLength(80);

            builder.HasOne(d => d.VehicleBodyType).WithMany(p => p.VehicleModels)
                .HasForeignKey(d => d.VehicleBodyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleModels_VehicleBodyTypes");

            builder.HasOne(d => d.VehicleMake).WithMany(p => p.VehicleModels)
                .HasForeignKey(d => d.VehicleMakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleModels_VehicleMakes");
        }
    }
}
