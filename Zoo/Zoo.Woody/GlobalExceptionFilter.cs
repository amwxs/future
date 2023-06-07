using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Zoo.Woody
{
    public class GlobalExceptionFilter: ExceptionFilterAttribute
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
            var response = new ApiResponse<object>();
            if (context.Exception is BusinessException businessException)
            {
                response.Code = businessException.Code;
                response.Message = businessException.Message;
            }
            else
            {
                _logger.LogError(context.Exception,"system error");

                response.Code = StatusCodes.Status500InternalServerError;
                response.Message = "Sorry, an error has occurred. Please contact the administrator.";
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
            context.ExceptionHandled = true;

        }
    }
}
