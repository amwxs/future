using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMW.ServiceDiscovery;

public static class DependencyInjection
{

    public static IServiceCollection UseConsul(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }

}
