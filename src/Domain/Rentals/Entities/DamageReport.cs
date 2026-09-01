using CarRental.Domain.Rentals.Enums;

namespace CarRental.Domain.Rentals.Entities;

public partial class DamageReport
{
    public long DamageReportId { get; set; }

    public long VehicleInspectionId { get; set; }

    public string Description { get; set; } = null!;

    public string LocationOnVehicle { get; set; } = null!;

    public DamageReportSeverity Severity { get; set; }

    public DamageReportStatus Status { get; set; } = DamageReportStatus.Reported;

    public decimal? EstimatedRepairCost { get; set; }

    public decimal? ActualRepairCost { get; set; }

    public DateTime? ResolvedAtUtc { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<DamageImage> DamageImages { get; set; } = new List<DamageImage>();

    public virtual VehicleInspection VehicleInspection { get; set; } = null!;
}
