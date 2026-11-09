using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Car_Rental_Management.OpenApi.Transformers;

internal sealed class VersionInfoTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var version = context.DocumentName;

        document.Info.Version = version;

        document.Info.Title = $"Car Rental Management Api {version}";

        return Task.CompletedTask;
    }
}
