namespace CarRental.Application.Common.Security;

public sealed class RefreshTokenSettings
{
    public const string SectionName = "RefreshToken";

    public int ExpirationInDays { get; set; } = 7;
}