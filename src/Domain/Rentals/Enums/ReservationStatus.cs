namespace CarRental.Domain.Rentals.Enums
{
    public enum ReservationStatus : byte
    {
        Unspecified = 0,
        Pending = 1,
        Confirmed = 2,
        Cancelled = 3,
        Expired = 4,
        Converted = 5
    }
}
