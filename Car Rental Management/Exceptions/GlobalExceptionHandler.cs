using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Exceptions
{
    public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails problem = exception switch
            {
                ValidationException ex =>
                    new ValidationProblemDetails(
                        ex.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToArray()))
                    {
                        Title = "Validation Failed",
                        Status = StatusCodes.Status400BadRequest
                    },

                _ => new ProblemDetails
                {
                    Title = "Server Error",
                    Detail = "An unexpected error occurred.",
                    Status = StatusCodes.Status500InternalServerError
                }
            };

            httpContext.Response.StatusCode = 
                problem.Status ?? StatusCodes.Status500InternalServerError;

            await problemDetailsService.WriteAsync(
                new ProblemDetailsContext 
                { 
                    HttpContext = httpContext,
                    ProblemDetails = problem
                });

            return true;
        }
    }
}
