using FluentValidation;

namespace CarRental.Application.Features.VehicleMakes.Commands.DeleteVehicleMake
{
    public sealed class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleMakeCommand>
    {
        public DeleteVehicleCommandValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Vehicle Make Id should be greater than zero!");
        }
    }
}
