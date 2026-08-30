namespace CarRental.Domain.Operations.Enums
{
    public enum MaintenanceType : byte
    {
        ScheduledService = 1,
        Repair = 2,
        Inspection = 3,
        Tires = 4,
        Cleaning = 5,
        Other = 6
    }
}
