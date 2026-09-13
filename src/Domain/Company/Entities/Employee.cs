using CarRental.Domain.Identity.Entities;
using CarRental.Domain.Operations.Entities;
using CarRental.Domain.Rentals.Entities;

namespace CarRental.Domain.Core.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public long UserId { get; set; }

    public int BranchId { get; set; }

    public string EmployeeNumber { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();

    public virtual ICollection<RentalAgreement> RentalAgreementCheckinEmployees { get; set; } = new List<RentalAgreement>();

    public virtual ICollection<RentalAgreement> RentalAgreementCheckoutEmployees { get; set; } = new List<RentalAgreement>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<VehicleInspection> VehicleInspections { get; set; } = new List<VehicleInspection>();
}
