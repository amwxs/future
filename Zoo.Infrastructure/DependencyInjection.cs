using Microsoft.Extensions.DependencyInjection;
using Zoo.Application.Core.Abstractions;
using Zoo.Infrastructure.Common;

namespace Zoo.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, MachineDateTime>();
        return services;
    }
}
