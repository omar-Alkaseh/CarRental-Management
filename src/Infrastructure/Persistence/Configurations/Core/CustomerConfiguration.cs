using CarRental.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Core
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "rental");

            builder.HasIndex(e => new { e.DriverLicenseCountryCode, e.DriverLicenseNumber }, "UQ_Customers_License").IsUnique();

            builder.HasIndex(e => e.UserId, "UQ_Customers_UserId").IsUnique();

            builder.Property(e => e.AddressLine).HasMaxLength(200);
            builder.Property(e => e.BlacklistReason).HasMaxLength(300);
            builder.Property(e => e.City).HasMaxLength(100);
            builder.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.DriverLicenseCountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.DriverLicenseNumber).HasMaxLength(50);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);

            builder.HasOne(d => d.User).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Customers_Users");
        }
    }
}
