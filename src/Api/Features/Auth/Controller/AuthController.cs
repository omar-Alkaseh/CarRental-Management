using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Car_Rental_Management.Features.Auth.Requests;
using Car_Rental_Management.Features.Auth.Responses;
using CarRental.Application.Common.Results;
using CarRental.Application.Features.Auth.Commands.Login;
using CarRental.Application.Features.Auth.Commands.Logout;
using CarRental.Application.Features.Auth.Commands.RefreshToken;
using CarRental.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Features.Auth.Controller;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/auth")]
[Tags("auth")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponse>> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var ipAddress = GetIpAddress();

        var command = new LoginCommand(
            request.Email,
            request.Password,
            ipAddress);

        var result = await sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponse>> RefreshToken(
        RefreshTokenRequest request, 
        CancellationToken cancellationToken)
    {
        var ipAddress = GetIpAddress();

        var command = new RefreshTokenCommand(
            request.RefreshToken,
            ipAddress);

        var result = await sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType<long>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<long>> Register(
        RegisterRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Password
        );

        var result = await sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result>> Logout(
        LogoutRequest request, 
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!long.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var ipAddress = GetIpAddress();

        var command = new LogoutCommand(
            userId,
            request.RefreshToken,
            ipAddress);

        await sender.Send(command, cancellationToken);

        return NoContent();
    }

    private string? GetIpAddress() =>
        HttpContext.Connection.RemoteIpAddress?.ToString();
}