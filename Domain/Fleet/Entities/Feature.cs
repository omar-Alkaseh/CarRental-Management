namespace CarRental.Domain.Fleet.Entities;

public partial class Feature
{
    public int FeatureId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
