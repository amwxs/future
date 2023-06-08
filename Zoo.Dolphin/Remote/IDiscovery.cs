using Consul;

namespace Zoo.Dolphin.Remote;

public interface IDiscovery
{
    Task<List<ServiceEntry>> GetServices(string serviceName);
}