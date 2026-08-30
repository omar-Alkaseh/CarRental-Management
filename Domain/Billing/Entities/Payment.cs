using CarRental.Domain.Billing.Enums;
using CarRental.Domain.Rentals.Entities;

namespace CarRental.Domain.Billing.Entities;

public partial class Payment
{
    public long PaymentId { get; set; }

    public string PaymentNumber { get; set; } = null!;

    public long ReservationId { get; set; }

    public long? InvoiceId { get; set; }

    public long? OriginalPaymentId { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public PaymentStatus Status { get; set; }

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string? GatewayName { get; set; }

    public string? TransactionReference { get; set; }

    public string? FailureReason { get; set; }

    public DateTime? PaidAtUtc { get; set; }

    public DateTime? FailedAtUtc { get; set; }

    public DateTime? RefundedAtUtc { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<Payment> InverseOriginalPayment { get; set; } = new List<Payment>();

    public virtual Invoice? Invoice { get; set; }

    public virtual Payment? OriginalPayment { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;
}
