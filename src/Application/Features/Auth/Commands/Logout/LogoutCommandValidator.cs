using FluentValidation;

namespace CarRental.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
        .NotEmpty()
        .WithMessage("refresh token is required.");
    }
}