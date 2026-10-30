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
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Vehicle-Makes")]
    [Tags("Vehicle-Make")]
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
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [ProducesResponseType(
            typeof(VehicleMakeResponse),
            StatusCodes.Status201Created)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
        [EndpointName("CreateVehicleMake")]
        [EndpointSummary("Create a new Vehicle Make")]
        [EndpointDescription("Creates a new vehicle manufacturer such as Toyota, BMW or Mercedes-Benz.")]
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
        [MapToApiVersion("1.0")]
        [ProducesResponseType(
            typeof(VehicleMakeResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        [EndpointName("UpdateVehicleMake")]
        [EndpointSummary("Update an existing vehicle make")]
        [EndpointDescription("Updated vehicle make information")]
        public async Task<IActionResult> Update(
            int Id,
            UpdateVehicleMakeRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateVehicleMakeCommand(Id, request.Name);

            var result = await sender.Send(command, cancellationToken);
            
            return Ok(result.Map(dto => dto.ToResponse()));
        }


        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Options()
        {
            Response.Headers["Allow"] =
                "GET, POST, PUT, DELETE, HEAD, OPTIONS";

            return NoContent();
        }
    }
}
