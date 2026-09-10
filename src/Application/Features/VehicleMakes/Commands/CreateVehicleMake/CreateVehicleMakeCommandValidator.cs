using FluentValidation;

namespace CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMakes
{
    public class CreateVehicleMakeCommandValidator : AbstractValidator<CreateVehicleMakeCommand>
    {
        public CreateVehicleMakeCommandValidator()
        {
            RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Vehicle Make Name should not be empty!")
            .Matches(@"^[A-Za-z]+(?:[ -][A-Za-z]+)*$")
            .WithMessage("Vehicle Make Name must contain letters separated only by single spaces or hyphens.");
        }
    }
}
