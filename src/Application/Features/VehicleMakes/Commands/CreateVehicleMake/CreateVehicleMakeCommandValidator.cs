using FluentValidation;

namespace CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMake
{
    public class CreateVehicleMakeCommandValidator : AbstractValidator<CreateVehicleMakeCommand>
    {
        public CreateVehicleMakeCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Vehicle Make Name should not be empty!")
            .Must(name => !name.All(char.IsDigit))
            .WithMessage("Vehicle Make Name should contain letters only.");
        }
    }
}
