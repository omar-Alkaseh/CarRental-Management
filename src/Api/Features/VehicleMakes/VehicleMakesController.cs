using Car_Rental_Management.Features.VehicleMakes.Requests;
using CarRental.Application.Common.Mappings;
using CarRental.Application.Features.VehicleMakes.Commands.CreateVehicleMakes;
using CarRental.Application.Features.VehicleMakes.Commands.UpdateVehicleMakes;
using CarRental.Application.Features.VehicleMakes.Mappings;
using CarRental.Application.Features.VehicleMakes.Requests;
using CarRental.Application.Features.VehicleMakes.Responses;
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<IActionResult> Update(
            int Id,
            UpdateVehicleMakeRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateVehicleMakeCommand(Id, request.Name);

            var result = await sender.Send(command, cancellationToken);
            
            return Ok(result.Map(dto => dto.ToResponse()));
        }
    }
}
