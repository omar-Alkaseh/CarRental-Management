using CarRental.Domain.Core.Entities;
using CarRental.Domain.Fleet.Enums;
using CarRental.Domain.Operations.Entities;
using CarRental.Domain.Rentals.Entities;

namespace CarRental.Domain.Fleet.Entities;

public partial class Vehicle
{
    public long VehicleId { get; set; }

    public int BranchId { get; set; }

    public int VehicleModelId { get; set; }

    public int VehicleCategoryId { get; set; }

    public string Vin { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public short ModelYear { get; set; }

    public string Color { get; set; } = null!;

    public VehicleTransmissionType TransmissionType { get; set; }

    public VehicleFuelType FuelType { get; set; }

    public int CurrentOdometerKm { get; set; }

    public decimal CurrentFuelLevel { get; set; }

    public VehicleOperationalStatus OperationalStatus { get; set; } = VehicleOperationalStatus.Available;

    public DateOnly? RegistrationExpiryDate { get; set; }

    public DateOnly? InsuranceExpiryDate { get; set; }

    public DateOnly? AcquiredAt { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();

    public virtual ICollection<RentalAgreement> RentalAgreements { get; set; } = new List<RentalAgreement>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual VehicleCategory VehicleCategory { get; set; } = null!;

    public virtual ICollection<VehicleImage> VehicleImages { get; set; } = new List<VehicleImage>();

    public virtual ICollection<VehicleInspection> VehicleInspections { get; set; } = new List<VehicleInspection>();

    public virtual VehicleModel VehicleModel { get; set; } = null!;

    public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
}
