using CarRental.Application.Common.Results;

namespace CarRental.Application.Features.Auth.Errors;

public static class AuthErrors
{
    public static Error InvalidCredentials() => 
        new (
            "Auth.InvalidCredential",
            "Invalid email or password.",
            ErrorType.Unauthorized
        );

    public static Error InvalidRefreshToken() => 
        new (
            "Auth.InvalidRefreshToken",
            "Invalid or expired refresh token.",
            ErrorType.Unauthorized
        );

    public static Error AccountInactive() =>
        new(
            "Auth.AccountInactive",
            "Your account is inactive. Please contact support.",
            ErrorType.Forbidden
        );

    public static Error AccountLocked() =>
        new(
            "Auth.AccountLocked",
            "Your account is temporarily locked. Please try again later.",
            ErrorType.Forbidden
        );

    public static Error EmailAlreadyExists() =>
        new(
            "Auth.ExistEmail",
            "This email already exist.",
            ErrorType.Conflict
        );

    public static Error RoleIsNotFound() =>
        new(
            "Auth.RolesNotExist",
            "This Role is not exist.",
            ErrorType.NotFound
        );
}