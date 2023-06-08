using Consul;

namespace Zoo.Dolphin.Remote
{
    public interface IServicesManager
    {
        Task<List<ServiceEntry>> GetServices(string serviceName);
        Task<List<ServiceEntry>> GetServicesFromRemote(string serviceName);
        void RefreshServicesCache();
    }
}