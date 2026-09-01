using CarRental.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Car_Rental_Management.Comman.Filters
{
    public sealed class ResultActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.Result is not ObjectResult objectResult)
                return;

            if (objectResult.Value is not Result result)
                return;

            if (result.IsSuccess)
                return;

            var error = result.Error!;

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

            executedContext.Result = new ObjectResult(
                new ProblemDetails
                {
                    Title = error.Code,
                    Detail = error.Message,
                    Status = statusCode

                })
            {
                StatusCode = statusCode
            };

        }
    }
}
