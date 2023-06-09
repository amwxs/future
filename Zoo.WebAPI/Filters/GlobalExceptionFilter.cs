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

        // 如果异常没有被处理则进行处理
        if (context.ExceptionHandled) return;
        var result = new Result<object>();
        if (context.Exception is CustException custException)
        {
            result.Code = custException.Code;
            result.Message = custException.Message;
        }
        else
        {
            _logger.LogError(context.Exception, "system error");

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
