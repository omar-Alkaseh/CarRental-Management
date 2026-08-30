using CarRental.Domain.Fleet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Fleet
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features", "fleet");

            builder.HasIndex(e => e.Name, "UQ_Features_Name").IsUnique();

            builder.Property(e => e.Description).HasMaxLength(300);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.Name).HasMaxLength(80);
        }
    }
}
