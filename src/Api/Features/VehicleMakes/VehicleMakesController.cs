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
        /// <summary>
        /// Creates a new vehicle make.
        /// </summary>
        /// <remarks>
        /// Creates a new vehicle manufacturer such as Toyota, BMW or Mercedes-Benz.
        /// The vehicle make name must be unique.
        /// </remarks>
        /// <param name="request">
        /// Vehicle make creation data.
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// The identifier of the newly created vehicle make.
        /// </returns>
        /// <response code="201">
        /// Vehicle make created successfully.
        /// </response>
        /// <response code="400">
        /// Validation failed.
        /// </response>
        /// <response code="409">
        /// A vehicle make with the same name already exists.
        /// </response>
        [HttpPost]
        [ProducesResponseType(
            typeof(VehicleMakeResponse),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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



        /// <summary>
        /// Updates an existing vehicle make.
        /// </summary>
        /// <param name="Id">
        /// The vehicle make identifier.
        /// </param>
        /// <param name="request">
        /// Updated vehicle make information.
        /// </param>
        /// <param name="cancellationToken"/>
        /// <response code="200">
        /// Vehicle make updated successfully.
        /// </response>
        /// <response code="400">
        /// Invalid request data.
        /// </response>
        /// <response code="404">
        /// Vehicle make was not found.
        /// </response>
        /// <response code="409">
        /// Another vehicle make already uses the same name.
        /// </response>

        [HttpPut("{Id:int}")]
        
        [ProducesResponseType(
            typeof(VehicleMakeResponse),
            StatusCodes.Status200OK)]
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
