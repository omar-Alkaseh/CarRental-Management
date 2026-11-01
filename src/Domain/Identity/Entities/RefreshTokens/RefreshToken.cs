using CarRental.Domain.Common.Exceptions;

namespace CarRental.Domain.Identity.Entities.RefreshTokens;

public partial class RefreshToken
{
    private RefreshToken() { }

    public long RefreshTokenId { get; private set; }
    public long UserId { get; private set; }

    public string TokenHash { get; private set; } = null!;
    public Guid JwtId { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    public DateTime? RevokedAtUtc { get; private set; }

    public string? ReplacedByTokenHash { get; private set; }
    public string? CreatedByIp { get; private set; }
    public string? RevokedByIp { get; private set; }
    public string? RevocationReason { get; private set; }

    public static RefreshToken Create(
        long userId,
        string tokenHash,
        Guid jwtId,
        DateTime nowUtc,
        DateTime expiresAtUtc,
        string? createdByIp)
    {
        if (userId <= 0)
            throw new DomainException(nameof(userId));

        ArgumentException.ThrowIfNullOrWhiteSpace(tokenHash);

        if (jwtId == Guid.Empty)
            throw new DomainException("Invalid JWT ID.");

        if (nowUtc <= expiresAtUtc)
            throw new DomainException("Expiration must be in the future.");

        return new RefreshToken
        {
            UserId = userId,
            TokenHash = tokenHash,
            JwtId = jwtId,
            CreatedAtUtc = nowUtc,
            ExpiresAtUtc = expiresAtUtc,
            CreatedByIp = createdByIp
        };
    }

    public bool IsExpired(DateTime nowUtc) =>
        ExpiresAtUtc <= nowUtc;

    public bool IsRevoked() =>
        RevokedAtUtc.HasValue;

    public bool IsActive(DateTime nowUtc) =>
        !IsExpired(nowUtc) && !IsRevoked();

    public void Revoke(
        DateTime nowUtc,
        string reason,
        string? revokedByIp,
        string? replacedByTokenHash = null)
    {
        if (!IsActive(nowUtc))
            throw new DomainException("Refresh token is not active.");
        
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);

        
        RevokedAtUtc = nowUtc;
        RevokedByIp = revokedByIp;
        RevocationReason = reason;
        ReplacedByTokenHash = replacedByTokenHash;
    }
}
