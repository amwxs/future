using Consul;

namespace AMW.ServiceDiscovery.Register.Client;

public interface IConsulClientProvider
{
    IConsulClient GetConsul();
}