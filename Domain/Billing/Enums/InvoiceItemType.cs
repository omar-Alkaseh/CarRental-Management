namespace CarRental.Domain.Billing.Enums
{
    public enum InvoiceItemType : byte
    {
        Rental = 1,
        Extra = 2,
        LateFee = 3,
        Fuel = 4,
        Damage = 5,
        Other = 6,
        Discount = 7,
        Tax = 8
    }
}
