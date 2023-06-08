using Consul;

namespace Zoo.Dolphin.Register.Client;

public interface IConsulClientProvider
{
    IConsulClient GetConsul();
}