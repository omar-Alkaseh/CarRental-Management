using CarRental.Application.Common.Results;

namespace CarRental.Application.Features.VehicleMakes.Erros
{
    public static class VehicleMakeErrors
    {
        public static Error NotFound(int id) =>
            new(
                "VehicleMake.NotFound",
                 $"Vehicle make with id '{id}' was not found.",
                 ErrorType.NotFound);

        public static Error NameAlreadyExists =>
            new(
            "VehicleMake.NameAlreadyExists",
            "Vehicle make name already exists.",
            ErrorType.Conflict);
    }
}
