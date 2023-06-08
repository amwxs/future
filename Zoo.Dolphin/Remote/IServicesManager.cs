using Consul;

namespace AMW.ServiceDiscovery.Remote;

public interface IServicesManager
{
    Task<List<ServiceEntry>> GetServices(string serviceName);
}