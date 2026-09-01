using Car_Rental_Management.Features.VehicleMakes.Requests;
using CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMake;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Features.VehicleMakes
{
    [Route("api/Vehicle-Makes")]
    [ApiController]
    public sealed class VehicleMakesController(ISender sender) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
            CreateVehicleMakeRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateVehicleMakeCommand(
                request.vehicleMakeName);

            var result = await sender.Send(
                command,
                cancellationToken);

            return Ok(result);
        }
    }
}
