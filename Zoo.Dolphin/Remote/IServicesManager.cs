using Consul;

namespace Zoo.Dolphin.Remote;

public interface IServicesManager
{
    Task<List<ServiceEntry>> GetServices(string serviceName);
}