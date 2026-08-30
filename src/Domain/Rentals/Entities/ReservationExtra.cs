namespace CarRental.Domain.Rentals.Entities;

public partial class ReservationExtra
{
    public long ReservationId { get; set; }

    public int ExtraId { get; set; }

    public string ExtraNameSnapshot { get; set; } = null!;

    public byte PricingUnitSnapshot { get; set; }

    public short Quantity { get; set; }

    public short ChargedUnits { get; set; }

    public decimal UnitPriceSnapshot { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Extra Extra { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;
}
