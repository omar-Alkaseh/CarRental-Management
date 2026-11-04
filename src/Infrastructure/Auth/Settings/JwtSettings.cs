namespace CarRental.Infrastructure.Auth.Settings;

public sealed class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int TokenExpirationInMinutes { get; set; }
    public string SecretKey { get; set; } = null!;
}