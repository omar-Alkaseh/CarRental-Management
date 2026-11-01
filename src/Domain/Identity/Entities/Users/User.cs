using CarRental.Domain.Common.Exceptions;

namespace CarRental.Domain.Identity.Entities.Users;

public sealed partial class User
{
    private readonly List<UserRole> _userRole = new();

    private User() { } // EF Core
    
    public long UserId { get; private set; }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string NormalizedEmail { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }

    public string PasswordHash { get; private set; } = null!;

    public bool IsEmailConfirmed { get; private set; }
    public bool IsActive { get; private set; }

    public short FailedLoginAttempts { get; private set; }
    public DateTime? LockoutUntilUtc { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }

    public byte[] RowVersion { get; private set; } = null!;

    public IReadOnlyCollection<UserRole> userRoles =>
        _userRole.AsReadOnly();

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        DateTime nowUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        email = email.Trim();

        return new User
        {
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = email,
            NormalizedEmail = email.ToUpperInvariant(),
            PasswordHash = passwordHash,
            IsActive = true,
            IsEmailConfirmed = false,
            CreatedAtUtc = nowUtc
        };
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        string? phoneNumber,
        DateTime nowUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        FirstName = firstName.Trim();
        LastName = LastName.Trim();
        PhoneNumber = phoneNumber?.Trim();
        UpdatedAtUtc = nowUtc;
    }

    public void ChangeEmail(string email, DateTime nowUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        email = email.Trim();

        var normalizedEmail = email.ToUpperInvariant();

        if (NormalizedEmail == normalizedEmail)
            return;

        Email = email;
        NormalizedEmail = normalizedEmail;
        IsEmailConfirmed = false;   
        UpdatedAtUtc = nowUtc;
    }

    public void ConfirmEmail(DateTime nowUtc)
    {
        IsEmailConfirmed = true;
        UpdatedAtUtc = nowUtc;
    }

    public void ChangePasswordHash(string passwordHash, DateTime nowUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        PasswordHash = passwordHash;
        UpdatedAtUtc = nowUtc;
    }

    public void Activate(DateTime nowUtc)
    {
        IsActive = true;
        UpdatedAtUtc = nowUtc;
    }

    public void DeActivate(DateTime nowUtc)
    {
        IsActive = false;
        UpdatedAtUtc = nowUtc;
    }

    public bool IsLockedOut(DateTime nowUtc) =>
        LockoutUntilUtc.HasValue
        && LockoutUntilUtc.Value > nowUtc;

    public bool CanSignIn(DateTime nowUtc) =>
        IsActive && !IsLockedOut(nowUtc);

    public void RecordFailedLogin(
        DateTime nowUtc,
        short maxAttempts,
        TimeSpan lockoutDuration)
    {
        if (maxAttempts <= 0 || lockoutDuration <= TimeSpan.Zero)
            throw new DomainException("Invalid lockout policy.");

        if (IsLockedOut(nowUtc))
            return;

        if (FailedLoginAttempts < short.MaxValue)
            FailedLoginAttempts ++;

        if (FailedLoginAttempts >= maxAttempts)
            LockoutUntilUtc = nowUtc.Add(lockoutDuration);
    }

    public void RecordSuccessfulLogin()
    {
        LockoutUntilUtc = null;
        FailedLoginAttempts = 0;
    }

    public void AssignRole(short roleId, DateTime nowUtc)
    {
        if (roleId <= 0)
            throw new DomainException(nameof(roleId));

        if (_userRole.Any(r => r.RoleId == roleId))
            throw new DomainException("Role is already assigned to user.");

        _userRole.Add(new UserRole(roleId, nowUtc));
    }

    public void RemoveRole(short roleId)
    {
        var userRole = _userRole
            .FirstOrDefault(r => r.RoleId == roleId);

        if(userRole is null)
            throw new DomainException("Role is not assigned to user.");

        _userRole.Remove(userRole);
    }
}
