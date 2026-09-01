using CarRental.Domain.Billing.Entities;
using CarRental.Domain.Core.Entities;
using CarRental.Domain.Fleet.Entities;
using CarRental.Domain.Identity.Entities;
using CarRental.Domain.Rentals.Enums;

namespace CarRental.Domain.Rentals.Entities;

public partial class Reservation
{
    public long ReservationId { get; set; }

    public string ReservationNumber { get; set; } = null!;

    public long CustomerId { get; set; }

    public int PickupBranchId { get; set; }

    public int ReturnBranchId { get; set; }

    public int VehicleCategoryId { get; set; }

    public long? AssignedVehicleId { get; set; }

    public DateTime PickupAtUtc { get; set; }

    public DateTime ExpectedReturnAtUtc { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    public short EstimatedRentalDays { get; set; }

    public decimal DailyRateSnapshot { get; set; }

    public decimal BaseAmount { get; set; }

    public decimal ExtrasAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal EstimatedTotalAmount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string? CustomerNotes { get; set; }

    public string? CancellationReason { get; set; }

    public DateTime? CancelledAtUtc { get; set; }

    public long? CancelledByUserId { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual Vehicle? AssignedVehicle { get; set; }

    public virtual User? CancelledByUser { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Branch PickupBranch { get; set; } = null!;

    public virtual RentalAgreement? RentalAgreement { get; set; }

    public virtual ICollection<ReservationExtra> ReservationExtras { get; set; } = new List<ReservationExtra>();

    public virtual Branch ReturnBranch { get; set; } = null!;

    public virtual VehicleCategory VehicleCategory { get; set; } = null!;
}
