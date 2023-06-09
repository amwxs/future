using Zoo.Application;
using Zoo.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplication();

builder.Services.AddControllers(c =>
{
    c.Filters.Add<ResultActionFilter>();
    c.Filters.Add<GlobalExceptionFilter>();
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
