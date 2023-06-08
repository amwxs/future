using AMW.ServiceDiscovery.Register.Client;
using Consul;
using Microsoft.Extensions.Logging;

namespace AMW.ServiceDiscovery.Remote;

public class ServicesManager : IDisposable, IServicesManager
{

    private readonly ILogger<ServicesManager> _logger;
    private readonly IConsulClientProvider _consulClientProvider;
    private readonly Timer _timer;

    public ServicesManager(IConsulClientProvider consulClientProvider, ILogger<ServicesManager> logger)
    {
        _consulClientProvider = consulClientProvider;
        _timer = new Timer(x => RefreshServicesCache(), null, 0, 3000);
        _logger = logger;
    }

    public Task<List<ServiceEntry>> GetServices(string serviceName)
    {
        return GetServicesFromRemote(serviceName);
    }

    protected async Task<List<ServiceEntry>> GetServicesFromRemote(string serviceName)
    {

        try
        {
            var response = await _consulClientProvider.GetConsul().Health.Service(serviceName, null, true).ConfigureAwait(false);
            var serviceEntries = response.Response.Where(x => x.Service.Tags == null || x.Service.Tags.All(x => x.ToLower() != "dev")).ToList();
            return serviceEntries;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No registration node found for service {serviceName}", serviceName);

            throw;
        }
    }

    private void RefreshServicesCache()
    {

    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
