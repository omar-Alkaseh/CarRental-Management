namespace CarRental.Domain.Billing.Enums
{
    public enum InvoiceStatus : byte
    {
        Draft = 1,
        Issued = 2,
        PartiallyPaid = 3,
        Paid = 4,
        Voided = 5
    }
}
