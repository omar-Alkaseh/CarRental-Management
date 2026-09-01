namespace CarRental.Domain.Billing.Enums
{
    public enum PaymentStatus : byte
    {
        Unspecified = 0,
        Pending = 1,
        Paid = 2,
        Failed = 3,
        Cancelled = 4,
        PartiallyRefunded = 5,
        Refunded = 6
    }
}
