namespace Zoo.Dolphin.Register.Options;

public class ConsulOptions
{
    public const string Section = "Consul";
    public string? AppId { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string[]? Tags { get; set; }
    public HealthCheck HealthCheck { get; set; } = new();
}
