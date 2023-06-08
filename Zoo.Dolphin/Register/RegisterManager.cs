using Consul;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zoo.Dolphin.Application;
using Zoo.Dolphin.Register.Client;
using Zoo.Dolphin.Register.Options;

namespace Zoo.Dolphin.Register;

public class RegisterManager : IRegisterManager
{
    private readonly IConsulClientProvider _consulClientProvider;
    private readonly IApplicationProvider _applicationInfoProvider;
    private readonly ConsulOptions _options;
    private readonly ILogger<RegisterManager> _logger;
    public RegisterManager(IConsulClientProvider consulClientProvider,
        IApplicationProvider applicationInfoProvider,
        IOptions<ConsulOptions> options,
        ILogger<RegisterManager> logger)
    {
        _consulClientProvider = consulClientProvider;
        _applicationInfoProvider = applicationInfoProvider;
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

        var appInfo = _applicationInfoProvider.GetAppInfo();

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
            ID = appInfo.SericeId,
            Name = appInfo.AppId,
            Address = appInfo.HostIp,
            Port = appInfo.Port,
            Tags = _options.Tags,
            Checks = new[] { check }
        };

        _consulClientProvider.GetConsul().Agent.ServiceRegister(registration).GetAwaiter().GetResult();
        _logger.LogInformation("Service registered successfully!");
    }

    public void DeRegister()
    {
        var appInfo = _applicationInfoProvider.GetAppInfo();
        _consulClientProvider.GetConsul().Agent.ServiceDeregister(appInfo.AppId).GetAwaiter().GetResult();
        _logger.LogInformation("Service unregistered successfully!");
    }
}
