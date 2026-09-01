namespace CarRental.Domain.Operations.Enums
{
    public enum MaintenanceStatus : byte
    {
        Unspecified = 0,
        Scheduled = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }
}
