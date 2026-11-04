using CarRental.Application.Common.Results;
using CarRental.Application.Features.Auth.DTOs;
using MediatR;

namespace CarRental.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<AuthDto>>;