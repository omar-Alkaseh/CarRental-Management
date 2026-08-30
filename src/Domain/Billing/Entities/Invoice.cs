using CarRental.Domain.Billing.Enums;
using CarRental.Domain.Rentals.Entities;

namespace CarRental.Domain.Billing.Entities;

public partial class Invoice
{
    public long InvoiceId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public long RentalAgreementId { get; set; }

    public InvoiceStatus Status { get; set; }

    public decimal Subtotal { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public DateTime? IssuedAtUtc { get; set; }

    public DateTime? DueAtUtc { get; set; }

    public DateTime? PaidAtUtc { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual RentalAgreement RentalAgreement { get; set; } = null!;
}
