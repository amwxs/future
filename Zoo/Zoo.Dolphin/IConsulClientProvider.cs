using Consul;

namespace Zoo.Dolphin
{
    public interface IConsulClientProvider
    {
        IConsulClient GetConsul();
    }
}