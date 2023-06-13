using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Core;

namespace Zoo.WebAPI
{
    public static class APIGloablBehavior
    {
        public static IMvcBuilder ConfigureGloablBehavior(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition
                = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {

#pragma warning disable CS8602 // 解引用可能出现空引用。
                    var erros = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .Select(x => new BizError { Filed = x.Key, Message = x.Value.Errors.First().ErrorMessage })
                    .ToList();
#pragma warning restore CS8602 // 解引用可能出现空引用。

                    return new ObjectResult(BizResult.Failure<object>("4000", "Parameter validation failed!", erros));
                };
            });
            return builder;
        }
    }
}
