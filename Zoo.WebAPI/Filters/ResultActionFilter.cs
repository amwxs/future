using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zoo.Domain.Core.Result;

namespace Zoo.WebAPI.Filters
{
    public class ResultActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is CustResult)
            {
                return;
            }

            var resut = context.Result as ObjectResult;
            var response = new CustResult<object>
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
