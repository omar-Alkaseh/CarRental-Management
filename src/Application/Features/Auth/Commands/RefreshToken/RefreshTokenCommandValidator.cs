using FluentValidation;

namespace CarRental.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
        .Cascade(CascadeMode.Stop)
        .NotEmpty()
            .WithMessage("Refresh token is required.")
        .MaximumLength(1024)
            .WithMessage("Refresh token is too long.");
    }
}