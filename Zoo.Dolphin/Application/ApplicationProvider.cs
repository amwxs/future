using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace AMW.ServiceDiscovery.Application;

public class ApplicationProvider : IApplicationProvider
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApplicationProvider> _logger;
    private readonly IServer _server;
    private static ApplicationOption? _applicationOption;
    public ApplicationProvider(IConfiguration configuration,
        ILogger<ApplicationProvider> logger,
        IServer server)
    {
        _configuration = configuration;
        _logger = logger;
        _server = server;
    }

    public ApplicationOption GetAppInfo()
    {
        try
        {
            if (_applicationOption != null)
            {
                return _applicationOption;
            }
            _applicationOption = new ApplicationOption
            {
                AppId = GetAppId(),
                SericeId = GetSericeId(),
                HostIp = GetHostIp(),
                Port = GetPort()
            };
            return _applicationOption;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetApplicationInfo Error");
            throw;
        }

    }

    private string GetAppId()
    {
        var appid = _configuration[AppConst.AppId];
        if (string.IsNullOrEmpty(appid))
        {
            throw new ArgumentNullException("Not find AppId");
        }
        return appid;
    }

    private static string GetHostIp()
    {
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        var ipAddresses = networkInterfaces
            .Where(ni => ni.NetworkInterfaceType != NetworkInterfaceType.Loopback && !ni.Description.ToLower().Contains("virtual"))
            .SelectMany(ni => ni.GetIPProperties().UnicastAddresses)
            .Where(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork)
            .Select(ua => ua.Address.ToString())
            .ToList();
        if (ipAddresses.Count > 0)
        {
            return ipAddresses[0];

        }
        throw new ArgumentNullException("Not Find host IP!");
    }

    private int GetPort()
    {
        var address = _server.Features.Get<IServerAddressesFeature>()?.Addresses.Select(p => new Uri(p));
        if (address == null || !address.Any())
        {
            throw new ArgumentNullException("Not Find Run Port!");
        }
        _logger.LogWarning(address.First().OriginalString);
        return address.First().Port;
    }


    private static string GetSericeId()
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
