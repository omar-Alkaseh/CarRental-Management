using CarRental.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Identity
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens", "auth");

            builder.HasIndex(e => new { e.UserId, e.ExpiresAtUtc }, "IX_RefreshTokens_UserId_ExpiresAtUtc");

            builder.HasIndex(e => e.TokenHash, "UQ_RefreshTokens_TokenHash").IsUnique();

            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.CreatedByIp)
                .HasMaxLength(45)
                .IsUnicode(false);
            builder.Property(e => e.ExpiresAtUtc).HasPrecision(0);
            builder.Property(e => e.ReplacedByTokenHash)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.RevocationReason).HasMaxLength(200);
            builder.Property(e => e.RevokedAtUtc).HasPrecision(0);
            builder.Property(e => e.RevokedByIp)
                .HasMaxLength(45)
                .IsUnicode(false);
            builder.Property(e => e.TokenHash)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength();

            builder.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RefreshTokens_Users");
        }
    }
}
