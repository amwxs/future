using Consul;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Zoo.Dolphin.Remote
{
    public class ServicesManager : IDisposable, IServicesManager
    {

        private readonly ConcurrentDictionary<string, List<ServiceEntry>> _servicesCache = new();
        private readonly ILogger<ServicesManager> _logger;
        private readonly IConsulClientProvider _consulClientProvider;
        private readonly Timer _timer;
        private bool _refreshFlag = false;

        public ServicesManager(IConsulClientProvider consulClientProvider, ILogger<ServicesManager> logger)
        {
            _consulClientProvider = consulClientProvider;
            _timer = new Timer(x => RefreshServicesCache(), null, 0, 3000);
            _logger = logger;
        }

        public async Task<List<ServiceEntry>> GetServices(string serviceName)
        {
            return _servicesCache.GetOrAdd(serviceName, await GetServicesFromRemote(serviceName));
        }

        public async Task<List<ServiceEntry>> GetServicesFromRemote(string serviceName)
        {

            try
            {
                var response = await _consulClientProvider.GetConsul().Health.Service(serviceName, null, true).ConfigureAwait(false);
                var serviceEntries = response.Response.Where(x => x.Service.Tags == null || x.Service.Tags.All(x => x.ToLower() != "development")).ToList();

                if (!serviceEntries.Any())
                {
                    throw new NullReferenceException($"No registration node found for service {serviceName}");
                }

                return serviceEntries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No registration node found for service {serviceName}", serviceName);

                throw;
            }
        }

        private async Task RefreshAsync(string serviceName)
        {
            try
            {
                _servicesCache.GetOrAdd(serviceName, await GetServicesFromRemote(serviceName));

            }
            catch (Exception ex)
            {
                _servicesCache.TryRemove(serviceName, out _);
                _logger.LogError(ex, "Failed to refresh {serviceName}.", serviceName);
            }
        }


        public void RefreshServicesCache()
        {

            if (_refreshFlag)
            {
                _refreshFlag = false;
                var serviceNames = _servicesCache.Keys;

                var tasks = new List<Task>();
                foreach (var serviceName in serviceNames)
                {
                    var t = RefreshAsync(serviceName);
                    tasks.Add(t);
                }
                Task.WaitAll(tasks.ToArray());

                _refreshFlag = true;
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
