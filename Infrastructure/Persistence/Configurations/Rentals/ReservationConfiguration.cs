using CarRental.Domain.Rentals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Rentals
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations", "rental");

            builder.HasIndex(e => new { e.AssignedVehicleId, e.Status, e.PickupAtUtc, e.ExpectedReturnAtUtc }, "IX_Reservations_AssignedVehicle_DateRange").HasFilter("([AssignedVehicleId] IS NOT NULL)");

            builder.HasIndex(e => new { e.PickupBranchId, e.VehicleCategoryId, e.Status, e.PickupAtUtc, e.ExpectedReturnAtUtc }, "IX_Reservations_AvailabilityByCategory");

            builder.HasIndex(e => new { e.CustomerId, e.CreatedAtUtc }, "IX_Reservations_CustomerId_CreatedAtUtc").IsDescending(false, true);

            builder.HasIndex(e => e.ReservationNumber, "UQ_Reservations_ReservationNumber").IsUnique();

            builder.Property(e => e.BaseAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.CancellationReason).HasMaxLength(500);
            builder.Property(e => e.CancelledAtUtc).HasPrecision(0);
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.CustomerNotes).HasMaxLength(500);
            builder.Property(e => e.DailyRateSnapshot).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.EstimatedTotalAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.ExpectedReturnAtUtc).HasPrecision(0);
            builder.Property(e => e.ExtrasAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.PickupAtUtc).HasPrecision(0);
            builder.Property(e => e.ReservationNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.Status).HasDefaultValue((byte)1);
            builder.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);

            builder.HasOne(d => d.AssignedVehicle).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.AssignedVehicleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Reservations_AssignedVehicles");

            builder.HasOne(d => d.CancelledByUser).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CancelledByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Reservations_CancelledByUsers");

            builder.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Reservations_Customers");

            builder.HasOne(d => d.PickupBranch).WithMany(p => p.ReservationPickupBranches)
                .HasForeignKey(d => d.PickupBranchId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Reservations_PickupBranches");

            builder.HasOne(d => d.ReturnBranch).WithMany(p => p.ReservationReturnBranches)
                .HasForeignKey(d => d.ReturnBranchId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Reservations_ReturnBranches");

            builder.HasOne(d => d.VehicleCategory).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.VehicleCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Reservations_VehicleCategories");
        }
    }
}
