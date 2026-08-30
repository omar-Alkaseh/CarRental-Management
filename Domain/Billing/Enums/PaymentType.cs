namespace CarRental.Domain.Billing.Enums
{
    public enum PaymentType : byte
    {
        Deposit = 1,
        RentalCharge = 2,
        AdditionalCharge = 3,
        Refund = 4
    }
}
