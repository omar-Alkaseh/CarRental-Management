using CarRental.Application.Common.Results;
using CarRental.Application.Features.Auth.DTOs;
using MediatR;

namespace CarRental.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(string Email, string Password, string? RevokeByIp) : IRequest<Result<AuthDto>>;