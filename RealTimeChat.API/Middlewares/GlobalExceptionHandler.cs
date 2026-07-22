using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Domain.Exceptions;

namespace RealTimeChat.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            var (statusCode, title) = exception switch
            {
                AppException appEx => (appEx.StatusCode, appEx.Title),
                _ => (StatusCodes.Status501NotImplemented, "Internal Server Error")
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
                _logger.LogError(exception, "Unhandled exception occurred");
            else
                _logger.LogWarning(exception, "Handled exception: {Title}", title);

            var problemDetails = new ProblemDetails()
            {
                Title = title,
                Status = statusCode,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };


            if (exception is ValidationException validationException)
                problemDetails.Extensions["errors"] = validationException.Errors;

            httpContext.Response.StatusCode = statusCode;

            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
