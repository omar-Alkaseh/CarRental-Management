using CarRental.Application.Common.Results;

namespace CarRental.Application.Common.Mappings;

public static class ResultMappings
{
    public static Result<TDestination> Map<TSource, TDestination>(
        this Result<TSource> result,
        Func<TSource, TDestination> mapper)
    {
        if (result.IsFailure)
            return result.Error!;

        return Result<TDestination>.Success(
            mapper(result.Value!));
    }
}