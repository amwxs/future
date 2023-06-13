using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zoo.Application.Core.Primitives;
using Zoo.Domain.Exceptions;

namespace Zoo.WebAPI.Filters;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        var result = new BizResult<object>();
        if (context.Exception is BizException bizException)
        {
            result.Code = bizException.Code;
            result.Message = bizException.Message;
        }
        else
        {
            _logger.LogError(context.Exception,"System error");

            result.Code = "5000";
            result.Message = "Sorry, an error has occurred. Please contact the administrator.";
        }

        context.Result = new ObjectResult(result)
        {
            StatusCode = StatusCodes.Status200OK
        };
        context.ExceptionHandled = true;

    }
}
