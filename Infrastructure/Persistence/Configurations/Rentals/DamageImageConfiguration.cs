using CarRental.Domain.Rentals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Rentals
{
    public class DamageImageConfiguration : IEntityTypeConfiguration<DamageImage>
    {
        public void Configure(EntityTypeBuilder<DamageImage> builder)
        {
            builder.ToTable("DamageImages", "rental");

            builder.Property(e => e.Caption).HasMaxLength(200);
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.ImageUrl).HasMaxLength(1000);

            builder.HasOne(d => d.DamageReport).WithMany(p => p.DamageImages)
                .HasForeignKey(d => d.DamageReportId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DamageImages_DamageReports");
        }
    }
}
