using CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMakes;
using FluentValidation.TestHelper;

namespace CarRenta.Application.UnitTests.Features.VehicleMakes.Commands.CreateVehicleMake
{
    public class CreateVehicleMakeValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        public async Task Validator_WhenNameIsEmpty_ShouldHaveValidationError(string name)
        {
            // Arrange
            var validator = new CreateVehicleMakeCommandValidator();

            var command = new CreateVehicleMakeCommand(name);

            // Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(n => n.Name)
                .WithErrorMessage("Vehicle Make Name should not be empty!");
        }


        [Theory]
        [InlineData("254")]
        [InlineData("Kia254")]
        [InlineData("Kia 254")]
        [InlineData("2 Kia")]
        public async Task Validator_WhenNameContainsDigits_ShouldHaveValidationError(string name)
        {
            // Arrange
            var validator = new CreateVehicleMakeCommandValidator();

            var command = new CreateVehicleMakeCommand(name);

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Vehicle Make Name must contain letters separated only by single spaces or hyphens.");
        }


        [Theory]
        [InlineData("Kia@")]
        [InlineData("@Kia")]
        [InlineData("Kia#$")]
        [InlineData("K#ia")]
        [InlineData("-Kia")]
        [InlineData("Kia-")]
        [InlineData(" Kia")]
        [InlineData("Kia ")]
        [InlineData("Land  Rover")]
        [InlineData("Mercedes--Benz")]
        public async Task Validator_WhenNameHasInvalidFormat_ShouldHaveValidationError(string name)
        {
            // Arrange
            var validator = new CreateVehicleMakeCommandValidator();

            var command = new CreateVehicleMakeCommand(name);

            // Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Vehicle Make Name must contain letters separated only by single spaces or hyphens.");
        }


        [Theory]
        [InlineData("Kia")]
        [InlineData("Toyota")]
        [InlineData("Land Rover")]
        [InlineData("Mercedes-Benz")]
        public async Task Validator_WhenNameIsValid_ShouldNotHaveValidationError(string name)
        {
            // Arrange

            var validator = new CreateVehicleMakeCommandValidator();

            var command = new CreateVehicleMakeCommand(name);

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert 

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}