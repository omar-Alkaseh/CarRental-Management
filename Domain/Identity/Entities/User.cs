using CarRental.Domain.Core.Entities;
using CarRental.Domain.Operations.Entities;
using CarRental.Domain.Rentals.Entities;

namespace CarRental.Domain.Identity.Entities;

public partial class User
{
    public long UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string NormalizedEmail { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string PasswordHash { get; set; } = null!;

    public bool IsEmailConfirmed { get; set; }

    public bool IsActive { get; set; }

    public short FailedLoginAttempts { get; set; }

    public DateTime? LockoutUntilUtc { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
