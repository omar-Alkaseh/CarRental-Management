using CarRental.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Core
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches", "core");

            builder.HasIndex(e => e.Code, "UQ_Branches_Code").IsUnique();

            builder.Property(e => e.AddressLine).HasMaxLength(200);
            builder.Property(e => e.City).HasMaxLength(100);
            builder.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.Email).HasMaxLength(256);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.PhoneNumber).HasMaxLength(30);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);
        }
    }
}
