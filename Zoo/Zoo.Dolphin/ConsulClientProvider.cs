using Consul;
using Microsoft.Extensions.Options;

namespace Zoo.Dolphin
{
    public class ConsulClientProvider : IConsulClientProvider
    {
        private readonly object _lockObj = new();
        private volatile IConsulClient? _consul;

        private readonly ConsulOptions _options;
        public ConsulClientProvider(IOptions<ConsulOptions> options)
        {
            _options = options.Value;
        }

        public IConsulClient GetConsul()
        {
            if (_consul != null) return _consul;
            lock (_lockObj)
            {
                if (_consul != null) return _consul;
                var consulAddress = _options.Address;
                if (string.IsNullOrEmpty(consulAddress))
                {
                    throw new ArgumentNullException(consulAddress,"Consul registration address not found!");
                }
                _consul = new ConsulClient(c =>
                {
                    c.Address = new Uri(consulAddress);
                });
            }
            return _consul;
        }
    }
}
