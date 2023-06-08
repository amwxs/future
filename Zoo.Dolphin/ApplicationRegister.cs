using Consul;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace Zoo.Dolphin
{

    public class ApplicationRegister: IHostedService
    {
        private readonly IConsulClientProvider _consulClientProvider;
        private readonly IApplicationInfoProvider _applicationInfoProvider;
        private readonly ILogger<ApplicationRegister> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ConsulOptions _options;
        public ApplicationRegister(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<ApplicationRegister> logger,
            IConsulClientProvider consulClientProvider,
            IApplicationInfoProvider applicationInfoProvider, IOptions<ConsulOptions> options)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
            _consulClientProvider = consulClientProvider;
            _applicationInfoProvider = applicationInfoProvider;
            _options = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            var client = _consulClientProvider.GetConsul();
            var registerId = GetRegisterId();

            _hostApplicationLifetime.ApplicationStarted.Register(() => Register(client, registerId));
            _hostApplicationLifetime.ApplicationStopping.Register(() => DeRegister(client, registerId));

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void Register(IConsulClient client, string registerId)
        {
            var check = new AgentServiceCheck
            {
                Interval = TimeSpan.FromSeconds(_options.HealthCheck.Interval),
                Timeout = TimeSpan.FromSeconds(_options.HealthCheck.Timeout),
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(_options.HealthCheck.Deregister)
            };

            var appInfo = _applicationInfoProvider.GetInfo();

            switch (_options.HealthCheck?.ToHealthCheckEnum())
            {
                case HealthCheckEnum.HTTP:
                    check.HTTP = $"http://{appInfo.HostIp}:{appInfo.Port}{_options.HealthCheck.Path}";
                    break;
                case HealthCheckEnum.GRPC:
                    check.GRPC = $"{appInfo.HostIp}:{appInfo.Port}";
                    break;
                case HealthCheckEnum.TCP:
                    check.TCP = $"{appInfo.HostIp}:{appInfo.Port}";
                    break;
                default:
                    break;
            }

            var registration = new AgentServiceRegistration()
            {
                ID = registerId,
                Name = appInfo.AppId,
                Address = appInfo.HostIp,
                Port = appInfo.Port,
                Tags = _options.Tags,
                Checks = new[] { check }
            };

            client.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
            _logger.LogInformation("Service registered successfully!");
        }

        private void DeRegister(IConsulClient client, string registerId)
        {
            client.Agent.ServiceDeregister(registerId).GetAwaiter().GetResult();
            _logger.LogInformation("Service unregistered successfully!");
        }

        private static string GetRegisterId()
        {
            const string currentFolder = "registration";
            var basePath = Directory.GetCurrentDirectory();
            var folderPath = Path.Combine(basePath, currentFolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var path = Path.Combine(basePath, currentFolder, ".id");
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path, Encoding.UTF8);
                if (lines.Length > 0 && !string.IsNullOrEmpty(lines[0]))
                    return lines[0];
            }

            var id = Guid.NewGuid().ToString();
            File.AppendAllLines(path, new[] { id });
            return id;
        }
    }
}