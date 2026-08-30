namespace CarRental.Domain.Fleet.Entities;

public partial class VehicleImage
{
    public long VehicleImageId { get; set; }

    public long VehicleId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? AltText { get; set; }

    public bool IsMain { get; set; }

    public short SortOrder { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
