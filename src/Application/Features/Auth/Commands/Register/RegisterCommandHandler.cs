using CarRental.Application.Common.Interfaces;
using CarRental.Application.Common.Results;
using CarRental.Application.Features.Auth.Errors;
using CarRental.Domain.Identity.Constant;
using CarRental.Domain.Identity.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandHandler(
    ICarRentalDbContext context,
    IPasswordHasherService passwordHasherService,
    TimeProvider time) : IRequestHandler<RegisterCommand, Result<long>>
{
    public async Task<Result<long>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToUpperInvariant();

        var emailExists = await context.Users.AnyAsync(
            u => u.NormalizedEmail == normalizedEmail, cancellationToken);

        if (emailExists)
            AuthErrors.EmailAlreadyExists();


        var existRole = await context.Roles.AnyAsync(
            r => r.RoleId == RoleConstant.Customer,
            cancellationToken);

        if (!existRole)
            AuthErrors.RoleIsNotFound();

        var nowUtc = time.GetUtcNow().UtcDateTime;

        var passwordHash = passwordHasherService.HashPassword(request.Password);

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash,
            nowUtc);

        user.AssignRole(RoleConstant.Customer, time.GetUtcNow().UtcDateTime);

        await context.Users.AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Result<long>.Success(user.UserId);
    }
}