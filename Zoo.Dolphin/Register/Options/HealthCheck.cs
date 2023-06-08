namespace AMW.ServiceDiscovery.Register.Options;

public class HealthCheck
{
    public string? Mode { get; set; }
    public string Path { get; set; } = "/health";
    public int Interval { get; set; } = 10;
    public int Timeout { get; set; } = 5;
    public int Deregister { get; set; } = 30;
    public HealthCheckEnum ToHealthCheckEnum()
    {
        return Mode?.ToLower() switch
        {
            "grpc" => HealthCheckEnum.GRPC,
            "http" => HealthCheckEnum.HTTP,
            "tcp" => HealthCheckEnum.TCP,
            _ => HealthCheckEnum.GRPC,
        };
    }
}
