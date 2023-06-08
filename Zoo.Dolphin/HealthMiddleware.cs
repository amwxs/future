using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;

namespace Zoo.Dolphin
{
    public class HealthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConsulOptions _options;
        public HealthMiddleware(RequestDelegate next,IOptions<ConsulOptions> options)
        {
            _options = options.Value;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value == _options.HealthCheck.Path)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync("I'm OK!");
            }
            else
            {
                await _next(context);
            }

        }
    }
}
