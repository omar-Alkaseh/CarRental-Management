using System.Collections.Frozen;

namespace Car_Rental_Management.OpenApi.Transformers;

public static class VersioningConstant
{
    public static readonly FrozenSet<string> Versions =
        new[]
        {
            "v1",
            "v2"
        }.ToFrozenSet();
}