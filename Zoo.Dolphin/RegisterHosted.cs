using Zoo.Dolphin.Register;
using Microsoft.Extensions.Hosting;

namespace Zoo.Dolphin;


public class RegisterHosted : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IServiceRegister _serviceRegister;
    public RegisterHosted(
        IHostApplicationLifetime hostApplicationLifetime,
        IServiceRegister registerManager)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _serviceRegister = registerManager;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStarted.Register(() => _serviceRegister.Register());
        _hostApplicationLifetime.ApplicationStopping.Register(() => _serviceRegister.DeRegister());

        return Task.CompletedTask;

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}