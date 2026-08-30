namespace CarRental.Domain.Rentals.Enums
{
    public enum ReservationStatus : byte
    {
        Pending = 1,
        Confirmed = 2,
        Cancelled = 3,
        Expired = 4,
        Converted = 5
    }
}
