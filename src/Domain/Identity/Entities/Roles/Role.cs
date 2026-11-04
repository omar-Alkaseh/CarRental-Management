using CarRental.Domain.Identity.Entities.Users;

namespace CarRental.Domain.Identity.Entities.Roles;

public partial class Role
{
    private Role() { }
    public short RoleId { get; private set; }

    public string Name { get; private set; } = null!;

    public string NormalizedName { get; private set; } = null!;

    public string? Description { get; private set; }

    public static Role Create(
        string name,
        string? description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        name = name.Trim();

        return new Role
        {
            Name = name,
            NormalizedName = name.ToUpperInvariant(),
            Description = description?.Trim()
        };
    }

    public void ChangeName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name.Trim();
        NormalizedName = name.ToUpperInvariant();
    }

    public void ChangeDescription(string? description)
    {
        Description = description;
    }
}
