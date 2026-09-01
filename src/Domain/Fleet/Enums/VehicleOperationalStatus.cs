namespace CarRental.Domain.Fleet.Enums
{
    public enum VehicleOperationalStatus : byte
    {
        Unspecified = 0,
        Available = 1,
        Rented = 2,
        Maintenance = 3,
        OutOfService = 4
    }
}
