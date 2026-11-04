using System.Data;
using FluentValidation;

namespace CarRental.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required.")
        .EmailAddress().WithMessage("Email format is invalid.")
        .MaximumLength(60).WithMessage("Email must not exceed 60 characters.")
        .MinimumLength(13).WithMessage("Email must be at least 13 characters.");

        RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Password is required.");
    }
}