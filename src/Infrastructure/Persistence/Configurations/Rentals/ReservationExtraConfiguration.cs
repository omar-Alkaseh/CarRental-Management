using CarRental.Domain.Rentals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Rentals
{
    public class ReservationExtraConfiguration : IEntityTypeConfiguration<ReservationExtra>
    {
        public void Configure(EntityTypeBuilder<ReservationExtra> builder)
        {
            builder.HasKey(e => new { e.ReservationId, e.ExtraId });

            builder.ToTable("ReservationExtras", "rental");

            builder.Property(e => e.ExtraNameSnapshot).HasMaxLength(100);
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.UnitPriceSnapshot).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Extra).WithMany(p => p.ReservationExtras)
                .HasForeignKey(d => d.ExtraId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReservationExtras_Extras");

            builder.HasOne(d => d.Reservation).WithMany(p => p.ReservationExtras)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReservationExtras_Reservations");
        }
    }
}
