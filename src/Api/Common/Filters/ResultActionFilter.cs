using System.Diagnostics;
using CarRental.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Car_Rental_Management.Common.Filters
{
    public sealed class ResultActionFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is not ObjectResult objectResult)
            {
                await next();
                return;
            }

            if (objectResult.Value is not Result result)
            {
                await next();
                return;
            }

            if (result.IsSuccess)
            {
                if (objectResult.Value is IResultValue resultValue)
                {
                    objectResult.Value = resultValue.Value;
                }
                await next();
                return;
            }

            var error = result.Error;

            var statusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,

                _ => StatusCodes.Status500InternalServerError
            };

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = "Request failed",
                Detail = statusCode == StatusCodes.Status500InternalServerError 
                    ? "An unexpected error occurred."
                    : error.Message,
                Instance = context.HttpContext.Request.Path
            };

            problem.Extensions["code"] = error.Code;

            problem.Extensions["traceId"] =
                Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;

            problem.Extensions["requestId"] =
                context.HttpContext.TraceIdentifier;

            context.Result = new ObjectResult(problem)
            {
                StatusCode = statusCode,
                ContentTypes = {"application/problem+json"}
            };

            await next();
        }
    }
}
