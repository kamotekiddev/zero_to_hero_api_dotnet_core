using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ZeroToHeroAPI.Exeptions;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        _logger.LogError(exception, "Unhandled exception occurred");

        int statusCode;
        string message;
        string errorType;

        switch (exception)
        {
            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                message = exception.Message;
                errorType = "NotFound";
                break;

            case ArgumentException or ArgumentNullException or ValidationException or InvalidCastException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = exception.Message;
                errorType = "BadRequest";
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = exception.Message;
                errorType = "ServerError";
                break;
        }

        var response = new
        {
            statusCode,
            message,
            errorType
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}