using CarRental.Domain.Fleet.Entities;
using CarRental.Domain.Rentals.Entities;

namespace CarRental.Domain.Core.Entities;

public partial class Branch
{
    public int BranchId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string AddressLine { get; set; } = null!;

    public string City { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Reservation> ReservationPickupBranches { get; set; } = new List<Reservation>();

    public virtual ICollection<Reservation> ReservationReturnBranches { get; set; } = new List<Reservation>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
