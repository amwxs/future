using Zoo.Dolphin.Register.Client;
using Zoo.Dolphin.Register.Options;
using Consul;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace Zoo.Dolphin.Register;

public class ServiceRegister : IServiceRegister
{
    private readonly IConsulClientProvider _consulClientProvider;
    private readonly IHostInfomation _hostInfomation;
    private readonly ConsulOptions _options;
    private readonly ILogger<ServiceRegister> _logger;
    public ServiceRegister(IConsulClientProvider consulClientProvider,
        IHostInfomation hostInfomation,
        IOptions<ConsulOptions> options,
        ILogger<ServiceRegister> logger)
    {
        _consulClientProvider = consulClientProvider;
        _hostInfomation = hostInfomation;
        _options = options.Value;
        _logger = logger;
    }

    public void Register()
    {
        var check = new AgentServiceCheck
        {
            Interval = TimeSpan.FromSeconds(_options.HealthCheck.Interval),
            Timeout = TimeSpan.FromSeconds(_options.HealthCheck.Timeout),
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(_options.HealthCheck.Deregister)
        };

        var hostIP = _hostInfomation.GetHostIp();
        var port = _hostInfomation.GetPort();
        switch (_options.HealthCheck?.ToHealthCheckEnum())
        {
            case HealthCheckEnum.HTTP:
                check.HTTP = $"http://{hostIP}:{port}{_options.HealthCheck.Path}";
                break;
            case HealthCheckEnum.GRPC:
                check.GRPC = $"{hostIP}:{port}";
                break;
            case HealthCheckEnum.TCP:
                check.TCP = $"{hostIP}:{port}";
                break;
            default:
                break;
        }
        var serviceId = GetServiceId();
        var registration = new AgentServiceRegistration()
        {
            ID = serviceId,
            Name = _options.AppId,
            Address = hostIP,
            Port = port,
            Tags = _options.Tags,
            Checks = new[] { check }
        };

        _consulClientProvider.GetConsul().Agent.ServiceRegister(registration).GetAwaiter().GetResult();
        _logger.LogInformation("Service registered successfully!");
    }

    public void DeRegister()
    {
        var serviceId = GetServiceId();
        _consulClientProvider.GetConsul().Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
        _logger.LogInformation("Service unregistered successfully!");
    }

    private static string GetServiceId()
    {
        var basePath = Directory.GetCurrentDirectory();
        var path = Path.Combine(basePath, ".id");
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
