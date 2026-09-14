using FluentValidation;

namespace CarRental.Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;

public sealed class UpdateVehicleMakeCommandValidator : AbstractValidator<UpdateVehicleMakeCommand>
{
    public UpdateVehicleMakeCommandValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThanOrEqualTo(1)
        .WithMessage("Vehicle Make Id should be greater than zero!");

        RuleFor(x => x.Name)
        .Cascade(CascadeMode.Stop)
        .NotEmpty()
        .WithMessage("Vehicle Make Name should not be empty!")
        .Matches(@"^[A-Za-z]+(?:[ -][A-Za-z]+)*$")
        .WithMessage("Vehicle Make Name must contain letters separated only by single spaces or hyphens.");
    }
}