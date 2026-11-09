using CarRental.Application.Common.Results;
using MediatR;

namespace CarRental.Application.Features.Auth.Commands.Logout;

public sealed record LogoutCommand(long UserId, string RefreshToken, string? RevokeByIP) : IRequest<Result>;