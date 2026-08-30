using CarRental.Domain.Identity.Entities;
using CarRental.Domain.Rentals.Entities;

namespace CarRental.Domain.Core.Entities;

public partial class Customer
{
    public long CustomerId { get; set; }

    public long UserId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string DriverLicenseNumber { get; set; } = null!;

    public string DriverLicenseCountryCode { get; set; } = null!;

    public DateOnly DriverLicenseExpiryDate { get; set; }

    public bool IsLicenseVerified { get; set; }

    public string? AddressLine { get; set; }

    public string? City { get; set; }

    public string? CountryCode { get; set; }

    public bool IsBlacklisted { get; set; }

    public string? BlacklistReason { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual User User { get; set; } = null!;
}
