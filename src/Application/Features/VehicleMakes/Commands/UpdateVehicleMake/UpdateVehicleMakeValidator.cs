using FluentValidation;

namespace CarRental.Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;

public sealed class UpdateVehicleMakeValidator : AbstractValidator<UpdateVehicleMakeCommand>
{
    public UpdateVehicleMakeValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThanOrEqualTo(1)
        .WithMessage("Vehicle Make Id should be greater than zero!");

        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Vehicle Make Name should not be empty!");
    }
}