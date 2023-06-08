using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zoo.Dolphin.Application;
using Zoo.Dolphin.Health;
using Zoo.Dolphin.Registration.Client;
using Zoo.Dolphin.Register.Client;
using Zoo.Dolphin.Register.Options;
using Zoo.Dolphin.Register;

namespace Zoo.Dolphin;

public static class DependencyInjection
{

    public static IServiceCollection AddDolphin(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<ConsulOptions>(configuration.GetSection(ConsulOptions.Section));

        services.TryAddSingleton<IConsulClientProvider, ConsulClientProvider>();
        services.TryAddSingleton<IApplicationProvider, ApplicationProvider>();
        services.TryAddSingleton<IRegisterManager, RegisterManager>();

        //services.TryAddSingleton<IServicesManager, ServicesManager>();

        services.AddHostedService<RegisterHosted>();


        services.AddGrpc();
        services.AddGrpcHealthChecks()
         .AddCheck("zoo.dolphin", () => HealthCheckResult.Healthy());

        return services;
    }
    /// <summary>
    /// 健康检查中间件
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseHealth(this WebApplication app)
    {
       
        app.MapGrpcHealthChecksService();
        app.UseMiddleware<HealthMiddleware>();
        return app;
    }
}
