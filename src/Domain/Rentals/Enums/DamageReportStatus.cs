namespace CarRental.Domain.Rentals.Enums
{
    public enum DamageReportStatus : byte
    {
        Unspecified = 0,
        Reported = 1,
        InRepair = 2,
        Repaired = 3,
        Waived = 4
    }
}
