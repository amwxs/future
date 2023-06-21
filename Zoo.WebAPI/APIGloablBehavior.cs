namespace Zoo.WebAPI
{
    public static class APIGloablBehavior
    {
        public static IMvcBuilder ConfigureGloablBehavior(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
            {
                //options.JsonSerializerOptions.DefaultIgnoreCondition
                //= System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                //options.InvalidModelStateResponseFactory = context =>
                //{
                //    var erros = context.ModelState
                //        .Where(x => x.Value?.Errors.Count > 0)
                //        .Select(x => new ValidationError(x.Key, x.Value.Errors.First().ErrorMessage))
                //        .ToList();

                //    return new ObjectResult(BizResult.Failure<object>("4001", "Parameters validation failed!", erros));
                //};
            });
            return builder;
        }
    }
}
