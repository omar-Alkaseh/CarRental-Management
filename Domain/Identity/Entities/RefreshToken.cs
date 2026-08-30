namespace CarRental.Domain.Identity.Entities;

public partial class RefreshToken
{
    public long RefreshTokenId { get; set; }

    public long UserId { get; set; }

    public string TokenHash { get; set; } = null!;

    public Guid JwtId { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime? RevokedAtUtc { get; set; }

    public string? ReplacedByTokenHash { get; set; }

    public string? CreatedByIp { get; set; }

    public string? RevokedByIp { get; set; }

    public string? RevocationReason { get; set; }

    public virtual User User { get; set; } = null!;
}
