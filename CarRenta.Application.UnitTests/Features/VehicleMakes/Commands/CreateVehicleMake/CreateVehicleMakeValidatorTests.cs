using CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMakes;
using FluentAssertions;

namespace CarRenta.Application.UnitTests.Features.VehicleMakes.Commands.CreateVehicleMake
{
    public class CreateVehicleMakeValidatorTests
    {
        //RuleFor(x => x.Name)
        //    .NotEmpty()
        //    .WithMessage("Vehicle Make Name should not be empty!")
        //    .Must(name => !name.All(char.IsDigit))
        //    .WithMessage("Vehicle Make Name should not contain only Ditigts.");
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Validator_WhenNameIsInvalid_ShouldHaveValidationError(string name)
        {
            // Arrange
            var validator = new CreateVehicleMakeCommandValidator();

            var command = new CreateVehicleMakeCommand(name);

            // Act

            var result = await validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
        }



        public async Task Validator_WhenNameIsValid_ShouldThrowValidationException()
        {

        }



        public async Task Validator_WhenNameContainsOnlyDigits_ShouldHaveValidationError()
        {

        }



        public async Task Validator_WhenNameContainsLettersAndDigits_ShouldHaveValidationError()
        {

        }
    }
}
