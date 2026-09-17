using CarRental.Application.Common.Behaviors;
using CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMakes;
using FluentAssertions;
using FluentValidation;
using MediatR;

namespace CarRental.Application.UnitTests.Common.Behaviors
{
    public class ValidationBehaviorTests
    {
        [Fact]
        public async Task Handle_WhenNoValidatorsExist_ShouldCallNext()
        {
            // Arrange

            var validators = Enumerable.Empty<IValidator<CreateVehicleMakeCommand>>();

            var behavior = new ValidationBehavior<CreateVehicleMakeCommand, int>(validators);

            var request = new CreateVehicleMakeCommand(string.Empty);

            RequestHandlerDelegate<int> next =
                (CancellationToken _) => Task.FromResult(1);

            // Act

            var result = await behavior.Handle(
                request,
                next,
                CancellationToken.None);

            // Assert

            result.Should().Be(1);
        }



        [Fact]
        public async Task Handle_WhenValidationSucceeds_ShouldCallNext()
        {
            // Arrange

            var validators = new List<IValidator<CreateVehicleMakeCommand>>()
            {
                new CreateVehicleMakeCommandValidator()
            };

            var behavior = new ValidationBehavior<CreateVehicleMakeCommand, int>(validators);

            var request = new CreateVehicleMakeCommand("kia");

            RequestHandlerDelegate<int> next =
                (CancellationToken _) => Task.FromResult(1);

            // Act

            var result = await behavior.Handle(
                request,
                next,
                CancellationToken.None);

            // Assert

            result.Should().Be(1);
        }



        [Fact]
        public async Task Handle_WhenValidationFails_ShouldThrowValidationException()
        {
            // Arrange

            var validators = new List<IValidator<CreateVehicleMakeCommand>>()
            {
                new CreateVehicleMakeCommandValidator()
            };

            var behavior = new ValidationBehavior<CreateVehicleMakeCommand, int>(validators);

            var request = new CreateVehicleMakeCommand("  2");

            RequestHandlerDelegate<int> next =
                (CancellationToken _) => Task.FromResult(1);

            // Act

            Func<Task> act = () => 
                 behavior.Handle(
                    request,
                    next,
                    CancellationToken.None);

            // Assert

            await act.Should().
                ThrowAsync<ValidationException>();
        }
        
    }
}
