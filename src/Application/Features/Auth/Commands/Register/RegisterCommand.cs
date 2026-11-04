using CarRental.Application.Common.Results;
using CarRental.Application.Features.Auth.DTOs;
using MediatR;

namespace CarRental.Application.Features.Auth.Commands.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password) : IRequest<Result<RegisterDto>>;
