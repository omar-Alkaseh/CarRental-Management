using CarRental.Application.Features.VehicleMakes.Commands.DeleteVehicleMake;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace CarRenta.Application.UnitTests.Features.VehicleMakes.Commands.DeleteVehicleMake
{
    public class DeleteVehicleMakeCommandValidatorTests
    {
        [Fact]
        public async Task Validator_WhenIdIsZero_ShouldHaveValidationError()
        {
            // Arrange

            var vehicleMakeId = 0;

            var validator = new DeleteVehicleCommandValidator();

            var command = new DeleteVehicleMakeCommand(vehicleMakeId);

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert

            result.ShouldHaveValidationErrorFor(e => e.Id)
                .WithErrorMessage("Vehicle Make Id should be greater than zero!");
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-152)]
        public async Task Validator_WhenIdIsNegative_ShouldHaveValidationError(int id)
        {
            // Arrange

            var validator = new DeleteVehicleCommandValidator();

            var command = new DeleteVehicleMakeCommand(id);

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert

            result.ShouldHaveValidationErrorFor(e => e.Id)
                .WithErrorMessage("Vehicle Make Id should be greater than zero!");
        }


        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(152)]
        public async Task Validator_WhenIdIsValid_ShouldNotHaveValidationError(int id)
        {
            // Arrange

            var validator = new DeleteVehicleCommandValidator();

            var command = new DeleteVehicleMakeCommand(id);

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert

            result.ShouldNotHaveValidationErrorFor(e => e.Id);
        }
    }
}
