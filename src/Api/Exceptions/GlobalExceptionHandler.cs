using CarRental.Domain.Common.Exceptions;
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
                        Detail = "One or more validation errors occurred.",
                        Status = StatusCodes.Status400BadRequest
                    },

                DomainException ex =>
                    new ProblemDetails
                    {
                        Title = "Domain Rule Error",
                        Detail = ex.Message,
                        Status = StatusCodes.Status400BadRequest
                    },

                _ => 
                    new ProblemDetails
                    {
                        Title = "Server Error",
                        Detail = "An unexpected error occurred.",
                        Status = StatusCodes.Status500InternalServerError
                    }
            };

            httpContext.Response.StatusCode = 
                problem.Status ?? StatusCodes.Status500InternalServerError;

            var context = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem
            };

            
            var written = 
                await problemDetailsService.TryWriteAsync(context);

            if(!written)
            {
                if (problem is ValidationProblemDetails validationProblem)
                {
                    await httpContext.Response.WriteAsJsonAsync(
                        validationProblem,
                        cancellationToken);
                }
                else
                {
                    await httpContext.Response.WriteAsJsonAsync(
                        problem,
                        cancellationToken);
                }
            }
            return true;
        }
    }
}
