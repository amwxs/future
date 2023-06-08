using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zoo.Woody
{
    public class ApiResponseActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is not ObjectResult objResult) return;
            
            if (objResult.Value is ApiResponse)
            {
                return;
            }

            var response = new ApiResponse<object>
            {
                Data = objResult.Value,
                Code = 0
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
