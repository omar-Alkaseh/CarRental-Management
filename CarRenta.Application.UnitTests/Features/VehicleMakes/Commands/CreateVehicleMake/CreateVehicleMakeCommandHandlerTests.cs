using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMakes;
using CarRental.Application.Features.VehicleMakes.Errors;
using CarRental.Domain.Fleet.Entities;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
namespace CarRenta.Application.UnitTests.Features.VehicleMakes.Commands.CreateVehicleMake
{
    public class CreateVehicleMakeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WhenNameDoesNotExists_ShouldCreateVehicleMake()
        {
            // Arrange

            var vehicleName = "Samsung";

            var vehicleMakes = new List<VehicleMake>();

            var vehicleMakeDbSet = vehicleMakes.BuildMockDbSet();

            var contextMock = new Mock<ICarRentalDbContext>();

            contextMock.
                Setup(x => x.VehicleMakes)
                .Returns(vehicleMakeDbSet.Object);

            VehicleMake? createdVehicleMake = null;

            vehicleMakeDbSet
                .Setup(x => x.AddAsync(
                    It.IsAny<VehicleMake>(),
                    It.IsAny<CancellationToken>()))
                .Callback<VehicleMake, CancellationToken>(
                    (vehicleMake, _) =>
                    {
                        createdVehicleMake = vehicleMake;
                    }
                );

            contextMock
                .Setup(v => v.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var handler = new CreateVehicleMakeCommandHandler(contextMock.Object);

            var command = new CreateVehicleMakeCommand(vehicleName);

            // Act

            Result<int> result = await handler.Handle(
                command, 
                CancellationToken.None);

            // Assert

            result.IsSuccess.Should().BeTrue();

            createdVehicleMake.Should().NotBeNull();
            createdVehicleMake!.Name.Should().Be(vehicleName);
            createdVehicleMake.IsActive.Should().BeTrue();

            vehicleMakeDbSet.Verify(
                x => x.AddAsync(
                    It.Is<VehicleMake>(
                        v => v.Name == vehicleName),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            contextMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }



        [Fact]
        public async Task Handle_WhenNameExists_ShouldReturnNameAlreadyExistsError()
        {
            // Arrange

            var vehicleName = "Kia";

            var vehicleMakes = new List<VehicleMake>()
            {
                VehicleMake.Create(vehicleName)
            };

            var vehicleMakeDbSet = vehicleMakes.BuildMockDbSet();

            var contextMock = new Mock<ICarRentalDbContext>();

            contextMock.
                Setup(x => x.VehicleMakes)
                .Returns(vehicleMakeDbSet.Object);


            var handler = new CreateVehicleMakeCommandHandler(contextMock.Object);

            var command = new CreateVehicleMakeCommand(vehicleName);

            // Act

            Result<int> result = 
                await handler.Handle(
                    command, 
                    CancellationToken.None);

            // Assert

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(
                VehicleMakeErrors.NameAlreadyExists);

            vehicleMakeDbSet.Verify(
                x => x.AddAsync(
                    It.IsAny<VehicleMake>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);

            contextMock.Verify(
                x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}