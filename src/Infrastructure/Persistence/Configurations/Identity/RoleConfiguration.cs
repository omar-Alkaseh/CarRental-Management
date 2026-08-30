using CarRental.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "auth");

            builder.HasIndex(e => e.NormalizedName, "UQ_Roles_NormalizedName").IsUnique();

            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.NormalizedName).HasMaxLength(50);
        }
    }
}
