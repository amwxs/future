using Zoo.Dolphin;
using Zoo.Woody;

namespace Sample.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers(c =>
            {
                c.Filters.Add<GlobalExceptionFilter>();
                c.Filters.Add<ApiResponseActionFilter>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            // 添加框架
            builder.Services.AddDolphin(builder.Configuration);

            var app = builder.Build();
            app.UseHealth();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            

            app.MapControllers();

            app.Run();
        }
    }


}