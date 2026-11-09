using CarRental.Application.Features.Auth.DTOs;
using FluentValidation;

namespace CarRental.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterDto>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("First Name is required.")
            .MaximumLength(20)
            .WithMessage("First name at must 20 character.");

        RuleFor(x => x.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Last Name is required.")
            .MaximumLength(20)
            .WithMessage("Last name at must 20 Character.");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid Email.");

        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Phone Number is required.")
            .Matches(@"^09[1-5]\d{7}$")
            .WithMessage("Invalid Libyan phone number. Must start with 091-095 and contain exactly 10 digits.");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MaximumLength(20)
            .WithMessage("Password At Must 20 Character.");
    }
}