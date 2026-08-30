namespace CarRental.Domain.Rentals.Entities;

public partial class DamageImage
{
    public long DamageImageId { get; set; }

    public long DamageReportId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? Caption { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public virtual DamageReport DamageReport { get; set; } = null!;
}
