using Microsoft.AspNetCore.Mvc;
using Zoo.Application;
using Zoo.Application.Core.Primitives;
using Zoo.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplication();

builder.Services.AddControllers(c =>
{
    //c.Filters.Add<ResultActionFilter>();
    c.Filters.Add<GlobalExceptionFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
#pragma warning disable CS8602 // 解引用可能出现空引用。
        var erros = context.ModelState
        .Where(x => x.Value?.Errors.Count > 0)
        .Select(x => new Error { Filed = x.Key, Message = x.Value.Errors.First().ErrorMessage })
        .ToList();
#pragma warning restore CS8602 // 解引用可能出现空引用。
        return new ObjectResult(Result.Failure<object>("4000", "Parameter validation failed!", erros));
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
