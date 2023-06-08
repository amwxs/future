using Zoo.Dolphin.Register.Client;
using Consul;
using Microsoft.Extensions.Logging;

namespace Zoo.Dolphin.Remote;

public class Discovery : IDiscovery, IDisposable
{

    private readonly ILogger<Discovery> _logger;
    private readonly IConsulClientProvider _consulClientProvider;
    private readonly Timer _timer;

    public Discovery(IConsulClientProvider consulClientProvider, ILogger<Discovery> logger)
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
