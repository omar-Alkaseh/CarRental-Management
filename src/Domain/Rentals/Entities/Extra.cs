using CarRental.Domain.Rentals.Enums;

namespace CarRental.Domain.Rentals.Entities;

public partial class Extra
{
    public int ExtraId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public ExtraPricingUnit PricingUnit { get; set; }

    public decimal UnitPrice { get; set; }

    public short MaximumQuantity { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<ReservationExtra> ReservationExtras { get; set; } = new List<ReservationExtra>();
}
