namespace AMW.ServiceDiscovery.Register.Options;

public class ConsulOptions
{
    public const string Section = "Consul";
    public string Address { get; set; } = string.Empty;
    public string[]? Tags { get; set; }
    public HealthCheck HealthCheck { get; set; } = new();
}
