using CarRental.Domain.Billing.Entities;
using CarRental.Domain.Core.Entities;
using CarRental.Domain.Fleet.Entities;
using CarRental.Domain.Rentals.Enums;

namespace CarRental.Domain.Rentals.Entities;

public partial class RentalAgreement
{
    public long RentalAgreementId { get; set; }

    public string RentalNumber { get; set; } = null!;

    public long ReservationId { get; set; }

    public long VehicleId { get; set; }

    public int CheckoutEmployeeId { get; set; }

    public int? CheckinEmployeeId { get; set; }

    public RentalAgreementStatus Status { get; set; } = RentalAgreementStatus.Open;

    public DateTime ActualPickupAtUtc { get; set; }

    public DateTime ExpectedReturnAtUtc { get; set; }

    public DateTime? ActualReturnAtUtc { get; set; }

    public int StartOdometerKm { get; set; }

    public int? EndOdometerKm { get; set; }

    public decimal StartFuelLevel { get; set; }

    public decimal? EndFuelLevel { get; set; }

    public decimal DailyRateSnapshot { get; set; }

    public decimal DepositAmount { get; set; }

    public decimal BaseAmount { get; set; }

    public decimal ExtrasAmount { get; set; }

    public decimal LateFee { get; set; }

    public decimal FuelCharge { get; set; }

    public decimal DamageCharge { get; set; }

    public decimal OtherCharges { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal? FinalAmount { get; set; }

    public string? PickupNotes { get; set; }

    public string? ReturnNotes { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual Employee? CheckinEmployee { get; set; }

    public virtual Employee CheckoutEmployee { get; set; } = null!;

    public virtual Invoice? Invoice { get; set; }

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;

    public virtual ICollection<VehicleInspection> VehicleInspections { get; set; } = new List<VehicleInspection>();
}
