using Sample.GrpcService.Services;
using Zoo.Dolphin;

namespace Sample.GrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDolphin(builder.Configuration);

            var app = builder.Build();

            app.UseHealth();

            app.MapGrpcService<GreeterService>();
            
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}