using CarRental.Domain.Core.Entities;
using CarRental.Domain.Fleet.Entities;
using CarRental.Domain.Operations.Enums;

namespace CarRental.Domain.Operations.Entities;

public partial class MaintenanceRecord
{
    public long MaintenanceRecordId { get; set; }

    public long VehicleId { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public MaintenanceType MaintenanceType { get; set; }

    public MaintenanceStatus Status { get; set; }

    public string Description { get; set; } = null!;

    public string? ServiceProvider { get; set; }

    public DateTime? ScheduledStartAtUtc { get; set; }

    public DateTime? ExpectedEndAtUtc { get; set; }

    public DateTime? StartedAtUtc { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public int OdometerKm { get; set; }

    public decimal? CostAmount { get; set; }

    public DateOnly? NextServiceDate { get; set; }

    public int? NextServiceOdometerKm { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual Employee CreatedByEmployee { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
