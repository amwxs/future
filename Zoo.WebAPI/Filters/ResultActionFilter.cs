using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zoo.Application.Core;

namespace Zoo.WebAPI.Filters
{
    public class ResultActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is BizResult)
            {
                return;
            }

            var resut = context.Result as ObjectResult;
            var response = new BizResult<object>
            {
                Data = resut?.Value,
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
