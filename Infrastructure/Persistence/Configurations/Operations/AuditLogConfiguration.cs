using CarRental.Domain.Operations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Operations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs", "ops");

            builder.HasIndex(e => new { e.EntityName, e.EntityId, e.OccurredAtUtc }, "IX_AuditLogs_Entity_OccurredAtUtc").IsDescending(false, false, true);

            builder.HasIndex(e => new { e.UserId, e.OccurredAtUtc }, "IX_AuditLogs_UserId_OccurredAtUtc").IsDescending(false, true);

            builder.Property(e => e.Action)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.EntityId).HasMaxLength(100);
            builder.Property(e => e.EntityName).HasMaxLength(128);
            builder.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false);
            builder.Property(e => e.OccurredAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.UserAgent).HasMaxLength(500);

            builder.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AuditLogs_Users");
        }
    }
}
