using CarRental.Domain.Billing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations.Billing
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.ToTable("InvoiceItems", "billing");

            builder.HasIndex(e => new { e.InvoiceId, e.SortOrder }, "IX_InvoiceItems_InvoiceId_SortOrder");

            builder.Property(e => e.Description).HasMaxLength(300);
            builder.Property(e => e.LineTotal).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");
            builder.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_InvoiceItems_Invoices");
        }
    }
}
