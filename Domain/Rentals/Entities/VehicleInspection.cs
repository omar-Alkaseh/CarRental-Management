using CarRental.Domain.Core.Entities;
using CarRental.Domain.Fleet.Entities;
using CarRental.Domain.Rentals.Enums;

namespace CarRental.Domain.Rentals.Entities;

public partial class VehicleInspection
{
    public long VehicleInspectionId { get; set; }

    public long RentalAgreementId { get; set; }

    public long VehicleId { get; set; }

    public int EmployeeId { get; set; }

    public VehicleInspectionType InspectionType { get; set; }

    public DateTime InspectedAtUtc { get; set; }

    public int OdometerKm { get; set; }

    public decimal FuelLevel { get; set; }

    public bool IsClean { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public virtual ICollection<DamageReport> DamageReports { get; set; } = new List<DamageReport>();

    public virtual Employee Employee { get; set; } = null!;

    public virtual RentalAgreement RentalAgreement { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
