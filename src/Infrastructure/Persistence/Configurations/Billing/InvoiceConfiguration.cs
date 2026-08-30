using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Billing.Entities;

namespace CarRental.Infrastructure.Data.Configurations.Billing
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices", "billing");

            builder.HasIndex(e => e.InvoiceNumber, "UQ_Invoices_InvoiceNumber").IsUnique();

            builder.HasIndex(e => e.RentalAgreementId, "UQ_Invoices_RentalAgreementId").IsUnique();

            builder.Property(e => e.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            builder.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            builder.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DueAtUtc).HasPrecision(0);
            builder.Property(e => e.InvoiceNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.IssuedAtUtc).HasPrecision(0);
            builder.Property(e => e.PaidAtUtc).HasPrecision(0);
            builder.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            builder.Property(e => e.Status).HasDefaultValue((byte)1);
            builder.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.UpdatedAtUtc).HasPrecision(0);

            builder.HasOne(d => d.RentalAgreement).WithOne(p => p.Invoice)
                .HasForeignKey<Invoice>(d => d.RentalAgreementId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Invoices_RentalAgreements");
        }
    }
}
