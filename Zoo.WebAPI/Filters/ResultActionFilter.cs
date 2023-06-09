using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zoo.Application.Core.Primitives;

namespace Zoo.WebAPI.Filters
{
    public class ResultActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is not ObjectResult objResult) return;

            if (objResult.Value is Result)
            {
                return;
            }

            var response = new Result<object>
            {
                Data = objResult.Value,
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
