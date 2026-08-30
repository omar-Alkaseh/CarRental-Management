using CarRental.Domain.Rentals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Rentals
{
    public class ExtraConfiguration : IEntityTypeConfiguration<Extra>
    {
        public void Configure(EntityTypeBuilder<Extra> builder)
        {
            builder.ToTable("Extras", "rental");

            builder.HasIndex(e => e.Name, "UQ_Extras_Name").IsUnique();

            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.MaximumQuantity).HasDefaultValue((short)1);
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);
        }
    }
}
