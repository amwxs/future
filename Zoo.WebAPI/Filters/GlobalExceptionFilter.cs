using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zoo.Domain.Core.Exceptions;
using Zoo.Domain.Core.Result;

namespace Zoo.WebAPI.Filters;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    const string _systemErrorCode = "5000";
    const string _systemErrorMessage = "Sorry, an error has occurred. Please contact the administrator.";
    const string _systemError = "SystemError";
    private readonly EventId _systemErrorEvent = new(5000);

    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        var result = new CustResult<object>();

        switch (context.Exception)
        {
            case CustException bizException:
                result.Code = bizException.Code;
                result.Message = bizException.Message;
                break;
            case CustValidationException custValidation:
                result.ValidationErrors = custValidation.ValidationErrors;
                break;

            default:
                _logger.LogError(_systemErrorEvent, context.Exception, _systemError);
                result.Code = _systemErrorCode;
                result.Message = _systemErrorMessage;
                break;
        }

        context.Result = new ObjectResult(result)
        {
            StatusCode = StatusCodes.Status200OK
        };
        context.ExceptionHandled = true;

    }
}
