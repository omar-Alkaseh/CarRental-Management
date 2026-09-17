using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using CarRental.Application.Features.VehicleMakes.Commands.DeleteVehicleMake;
using CarRental.Application.Features.VehicleMakes.Errors;
using CarRental.Domain.Fleet.Entities;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace CarRenta.Application.UnitTests.Features.VehicleMakes.Commands.DeleteVehicleMake
{
    public class DeleteVehicleMakeCommandHandlerTests
    {
        [Fact]
        public async Task Handler_WhenVehicleMakeDoesNotExsit_ShouldReturnNotFound()
        {
            // Arrange

            var vehicleMakeId = 3;

            var vehicleMakes = new List<VehicleMake>()
            {
                CreateVehicleMake(1, "Kia"),
                CreateVehicleMake(2, "Bmw")
            };

            var vehicleMakeDbSet = vehicleMakes.BuildMockDbSet();

            var contextMock = new Mock<ICarRentalDbContext>();

            contextMock
                .Setup(x => x.VehicleMakes)
                .Returns(vehicleMakeDbSet.Object);

            var handler = new DeleteVehicleMakeCommandHandler(contextMock.Object);

            var command = new DeleteVehicleMakeCommand(
                Id: vehicleMakeId);

            // Act

            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert

            result.IsFailure.Should().BeTrue();
            
            result.Error.Should().Be(
                VehicleMakeErrors.NotFound(vehicleMakeId));

            contextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Never());
        }



        [Fact]
        public async Task Handler_WhenVehicleMakeExsits_ShouldDeactivateVehicleMake()
        {
            // Arrange

            var vehicleMakeId = 1;

            var vehicleMakes = new List<VehicleMake>()
            {
                CreateVehicleMake(vehicleMakeId, "Kia"),
                CreateVehicleMake(2, "Bmw")
            };

            var vehicleMakeDbSet = vehicleMakes.BuildMockDbSet();

            var contextMock = new Mock<ICarRentalDbContext>();

            contextMock
                .Setup(x => x.VehicleMakes)
                .Returns(vehicleMakeDbSet.Object);

            var handler = new DeleteVehicleMakeCommandHandler(contextMock.Object);

            var command = new DeleteVehicleMakeCommand(
                Id: vehicleMakeId);

            // Act

            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert

            result.IsSuccess.Should().BeTrue();
            result.Error.Should().Be(Error.None);

            contextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once());
        }

        private static VehicleMake CreateVehicleMake(int id, string name)
        {
            var vehicleMake = VehicleMake.Create(name);

            typeof(VehicleMake)
                .GetProperty(nameof(VehicleMake.Id))!
                .SetValue(vehicleMake, id);

            return vehicleMake;
        }
    }
}
