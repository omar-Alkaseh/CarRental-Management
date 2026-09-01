using CarRental.Domain.Rentals.Entities;
using CarRental.Domain.Rentals.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Rentals
{
    public class RentalAgreementConfiguration : IEntityTypeConfiguration<RentalAgreement>
    {
        public void Configure(EntityTypeBuilder<RentalAgreement> builder)
        {
            builder.ToTable("RentalAgreements", "rental");

            builder.HasIndex(e => new { e.Status, e.ExpectedReturnAtUtc }, "IX_RentalAgreements_Status_ExpectedReturnAtUtc");

            builder.HasIndex(e => e.RentalNumber, "UQ_RentalAgreements_RentalNumber").IsUnique();

            builder.HasIndex(e => e.ReservationId, "UQ_RentalAgreements_ReservationId").IsUnique();

            builder.HasIndex(e => e.VehicleId, "UX_RentalAgreements_OneOpenRentalPerVehicle")
                .IsUnique()
                .HasFilter("([Status]=CONVERT([tinyint],(1)))");

            builder.Property(e => e.ActualPickupAtUtc).HasPrecision(0);
            builder.Property(e => e.ActualReturnAtUtc).HasPrecision(0);
            builder.Property(e => e.BaseAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.DailyRateSnapshot).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DamageCharge).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DepositAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.EndFuelLevel).HasColumnType("decimal(5, 2)");
            builder.Property(e => e.ExpectedReturnAtUtc).HasPrecision(0);
            builder.Property(e => e.ExtrasAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.FinalAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.FuelCharge).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.LateFee).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.OtherCharges).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.PickupNotes).HasMaxLength(1000);
            builder.Property(e => e.RentalNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.ReturnNotes).HasMaxLength(1000);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.StartFuelLevel).HasColumnType("decimal(5, 2)");
            builder.Property(e => e.Status).HasDefaultValue(RentalAgreementStatus.Open).HasSentinel(RentalAgreementStatus.Unspecified);
            builder.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);

            builder.HasOne(d => d.CheckinEmployee).WithMany(p => p.RentalAgreementCheckinEmployees)
                .HasForeignKey(d => d.CheckinEmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RentalAgreements_CheckinEmployees");

            builder.HasOne(d => d.CheckoutEmployee).WithMany(p => p.RentalAgreementCheckoutEmployees)
                .HasForeignKey(d => d.CheckoutEmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RentalAgreements_CheckoutEmployees");

            builder.HasOne(d => d.Reservation).WithOne(p => p.RentalAgreement)
                .HasForeignKey<RentalAgreement>(d => d.ReservationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RentalAgreements_Reservations");

            builder.HasOne(d => d.Vehicle).WithMany(p => p.RentalAgreements)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RentalAgreements_Vehicles");
        }
    }
}
