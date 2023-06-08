namespace Zoo.Dolphin;

public class ConsulOptions
{
    public const string Section = "Consul";
    public string Address { get; set; } = string.Empty;
    public string[]? Tags { get; set; }
    public HealthCheck HealthCheck { get; set; } = new();
}

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
public enum HealthCheckEnum
{
    HTTP =0,
    GRPC =1,
    TCP = 2
}
