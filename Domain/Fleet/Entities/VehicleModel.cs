namespace CarRental.Domain.Fleet.Entities;

public partial class VehicleModel
{
    public int VehicleModelId { get; set; }

    public int VehicleMakeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public short VehicleBodyTypeId { get; set; }

    public virtual VehicleBodyType VehicleBodyType { get; set; } = null!;

    public virtual VehicleMake VehicleMake { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
