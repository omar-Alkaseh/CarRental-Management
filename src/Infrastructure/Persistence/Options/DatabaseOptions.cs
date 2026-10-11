namespace CarRental.Infrastructure.Persistence.Options;

public sealed class DatabaseOptions
{
    public const string SectionName = "ConnectionStrings";

    public string CarRentalDatabase { get; init; } = string.Empty;
} 