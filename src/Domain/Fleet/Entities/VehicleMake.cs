using CarRental.Domain.Common.Exceptions;
using System.Text.RegularExpressions;

namespace CarRental.Domain.Fleet.Entities;

public partial class VehicleMake
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public virtual ICollection<VehicleModel> VehicleModels { get; set; }
        = new List<VehicleModel>();

    private VehicleMake() { }

    private VehicleMake(string name)
    {
        Name = name;
        IsActive = true;
    }

    public static VehicleMake Create(string name)
    {
        name = NormalizeName(name);

        ValidateName(name);

        return new VehicleMake(name);
    }

    public void ChangeName(string name)
    {
        name = NormalizeName(name);

        ValidateName(name);

        Name = name;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    private static string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(
                "Vehicle make name cannot be empty.");

        return name.Trim();
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(
                "Vehicle make name cannot be empty.");

        if (!Regex.IsMatch(name, @"^[A-Za-z]+(?:[ -][A-Za-z]+)*$"))
            throw new DomainException(
                "Vehicle make name is invalid.");
    }
}