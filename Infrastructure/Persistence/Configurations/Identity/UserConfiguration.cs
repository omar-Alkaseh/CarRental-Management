using CarRental.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "auth");

            builder.HasIndex(e => e.NormalizedEmail, "UQ_Users_NormalizedEmail").IsUnique();

            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.Email).HasMaxLength(256);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.Property(e => e.LockoutUntilUtc).HasPrecision(0);
            builder.Property(e => e.NormalizedEmail).HasMaxLength(256);
            builder.Property(e => e.PasswordHash).HasMaxLength(500);
            builder.Property(e => e.PhoneNumber).HasMaxLength(30);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);
        }
    }
}
