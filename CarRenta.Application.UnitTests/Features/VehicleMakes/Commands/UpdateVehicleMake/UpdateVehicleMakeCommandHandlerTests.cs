using Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;
using CarRental.Application.Common.Interfaces;
using CarRental.Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;
using CarRental.Application.Features.VehicleMakes.Errors;
using CarRental.Domain.Fleet.Entities;
using FluentAssertions;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using System.Runtime.InteropServices;

namespace CarRenta.Application.UnitTests.Features.VehicleMakes.Commands.UpdateVehicleMake
{
    public class UpdateVehicleMakeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WhenVehicleMakeIdDoesNotExist_ShouldReturnNotFoundError()
        {
            // Arrange

            var vehicleMakeId = 100;

            var vehicleMakes = new List<VehicleMake>();

            var vehicleMakeDbSet = vehicleMakes.BuildMockDbSet();

            var contextMock = new Mock<ICarRentalDbContext>();

            contextMock
                .Setup(x => x.VehicleMakes)
                .Returns(vehicleMakeDbSet.Object);

            var handler = new UpdateVehicleMakeCommandHandler(contextMock.Object);

            var command = new UpdateVehicleMakeCommand(
                Id: vehicleMakeId,
                Name: "Kia");

            // Act

            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should()
                .Be(VehicleMakeErrors.NotFound(vehicleMakeId));

            contextMock.Verify(
                v => v.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Never());
        }


        [Fact]
        public async Task Handle_WhenNameAlreadyExists_ShouldReturnConflictError()
        {
            // Arrange

            var vehicleMakeId = 3;

            var vehicleMakes = new List<VehicleMake>()
            {
                CreateVehicleMake(3, "Toyota"),
                CreateVehicleMake(5, "Kia")
            };

            var vehicleMakeDbSet = vehicleMakes.BuildMockDbSet();

            var contextMock = new Mock<ICarRentalDbContext>();

            contextMock
                .Setup(x => x.VehicleMakes)
                .Returns(vehicleMakeDbSet.Object);

            var handler = new UpdateVehicleMakeCommandHandler(contextMock.Object);

            var command = new UpdateVehicleMakeCommand(
                Id: vehicleMakeId,
                Name: "Kia");

            // Act

            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert

            result.IsFailure.Should().BeTrue();

            result.Error.Should()
                .Be(VehicleMakeErrors.NameAlreadyExists);

            contextMock.Verify(
                v => v.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Never());
        }



        [Fact]
        public async Task Handle_WhenVehicleMakeExistsAndNameDoesNotExist_ShouldUpdateVehicleMake()
        {
            // Arrange

            var vehicleMakeId = 3;
            var newVehicleMakeName = "Toyota";

            var vehicleMakes = new List<VehicleMake>()
            {
                CreateVehicleMake(vehicleMakeId, "Kia"),
                CreateVehicleMake(5, "Bmw")
            };

            var vehicleMakeDbSet = vehicleMakes.BuildMockDbSet();

            var contextMock = new Mock<ICarRentalDbContext>();

            contextMock
                .Setup(x => x.VehicleMakes)
                .Returns(vehicleMakeDbSet.Object);

            var handler = new UpdateVehicleMakeCommandHandler(contextMock.Object);

            var command = new UpdateVehicleMakeCommand(
                Id: vehicleMakeId,
                Name: newVehicleMakeName);

            // Act

            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(vehicleMakeId);
            result.Value.Name.Should().Be(newVehicleMakeName);


            contextMock.Verify(
                v => v.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }



        [Fact]
        public async Task Handle_WhenNameBelongsToSameVehicleMake_ShouldSucceed()
        {
            // Arrange

            var vehicleMakeId = 3;
            var vehicleMakeName = "Kia";

            var vehicleMakes = new List<VehicleMake>()
            {
                CreateVehicleMake(vehicleMakeId, vehicleMakeName),
                CreateVehicleMake(4, "Toyota")
            };

            var vehicleMakeDbSet = vehicleMakes.BuildMockDbSet();

            var contextMock = new Mock<ICarRentalDbContext>();

            contextMock
                .Setup(x => x.VehicleMakes)
                .Returns(vehicleMakeDbSet.Object);

            var handler = new UpdateVehicleMakeCommandHandler(contextMock.Object);

            var command = new UpdateVehicleMakeCommand(
                Id: vehicleMakeId,
                Name: vehicleMakeName);

            // Act

            var result = await handler.Handle(
                command,
                CancellationToken.None);

            // Assert

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(vehicleMakeId);
            result.Value!.Name.Should().Be(vehicleMakeName);

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
