using CarRental.Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;
using FluentValidation.TestHelper;

namespace CarRenta.Application.UnitTests.Features.VehicleMakes.Commands.UpdateVehicleMake
{
    public class UpdateVehicleMakeCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public async Task Validate_WhenNameIsEmpty_ShouldHaveValidationError(string name)
        {
            // arrange

            var validator = new UpdateVehicleMakeCommandValidator();

            var command = new UpdateVehicleMakeCommand(2, name);

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert

            result.ShouldHaveValidationErrorFor(n => n.Name)
                .WithErrorMessage("Vehicle Make Name should not be empty!");
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
        [InlineData("Kia1")]
        [InlineData("1Kia")]
        [InlineData("K1ia")]
        [InlineData("BMW123")]
        public async Task Validate_WhenNameHasInvalidFormat_ShouldHaveValidationError(string name)
        {
            // arrange

            var validator = new UpdateVehicleMakeCommandValidator();

            var command = new UpdateVehicleMakeCommand(2, name);

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert

            result.ShouldHaveValidationErrorFor(n => n.Name)
                .WithErrorMessage("Vehicle Make Name must contain letters separated only by single spaces or hyphens.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-1000)]
        public async Task Validate_WhenIdIsLessThanOne_ShouldHaveValidationError(int id)
        {
            // arrange

            var validator = new UpdateVehicleMakeCommandValidator();

            var command = new UpdateVehicleMakeCommand(id, "Kia");

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert

            result.ShouldHaveValidationErrorFor(n => n.Id)
                .WithErrorMessage("Vehicle Make Id should be greater than zero!");
        }


        [Theory]
        [InlineData(1, "Kia")]
        [InlineData(2, "Bmw")]
        [InlineData(3, "Toyota")]
        [InlineData(4, "Land Rover")]
        [InlineData(5, "Mercedes-Benz")]
        [InlineData(6, "Rolls-Royce")]
        public async Task Validate_WhenIdAndNameAreValid_ShouldNotHaveValidationError(int id, string name)
        {
            // arrange

            var validator = new UpdateVehicleMakeCommandValidator();

            var command = new UpdateVehicleMakeCommand(id, name);

            // Act

            var result = await validator.TestValidateAsync(command);

            // Assert

            result.ShouldNotHaveValidationErrorFor(x => x.Id);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
