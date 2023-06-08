namespace AMW.ServiceDiscovery.Remote.Http;

public interface IRemoteHttp
{
    T GetAsync<T>(string url, Dictionary<string, string> querys, Dictionary<string, string> headers);
}
