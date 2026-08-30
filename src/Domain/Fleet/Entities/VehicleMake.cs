namespace CarRental.Domain.Fleet.Entities;

public partial class VehicleMake
{
    public int VehicleMakeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();
}
