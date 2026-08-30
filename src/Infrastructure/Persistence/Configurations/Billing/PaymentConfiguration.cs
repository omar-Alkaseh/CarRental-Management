using CarRental.Domain.Billing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Billing
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", "billing");

            builder.HasIndex(e => new { e.InvoiceId, e.Status }, "IX_Payments_InvoiceId_Status").HasFilter("([InvoiceId] IS NOT NULL)");

            builder.HasIndex(e => new { e.ReservationId, e.Status }, "IX_Payments_ReservationId_Status");

            builder.HasIndex(e => e.PaymentNumber, "UQ_Payments_PaymentNumber").IsUnique();

            builder.HasIndex(e => e.TransactionReference, "UX_Payments_TransactionReference")
                .IsUnique()
                .HasFilter("([TransactionReference] IS NOT NULL)");

            builder.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.FailedAtUtc).HasPrecision(0);
            builder.Property(e => e.FailureReason).HasMaxLength(500);
            builder.Property(e => e.GatewayName).HasMaxLength(100);
            builder.Property(e => e.PaidAtUtc).HasPrecision(0);
            builder.Property(e => e.PaymentNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.RefundedAtUtc).HasPrecision(0);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.Status).HasDefaultValue((byte)1);
            builder.Property(e => e.TransactionReference).HasMaxLength(200);
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);

            builder.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_Payments_Invoices");

            builder.HasOne(d => d.OriginalPayment).WithMany(p => p.InverseOriginalPayment)
                .HasForeignKey(d => d.OriginalPaymentId)
                .HasConstraintName("FK_Payments_OriginalPayments");

            builder.HasOne(d => d.Reservation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Payments_Reservations");
        }
    }
}
