using Zoo.Dolphin.Register;
using Microsoft.Extensions.Hosting;

namespace Zoo.Dolphin;


public class RegisterHosted : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IRegisterManager _registerManager;
    public RegisterHosted(
        IHostApplicationLifetime hostApplicationLifetime,
        IRegisterManager registerManager)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _registerManager = registerManager;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hostApplicationLifetime.ApplicationStarted.Register(() => _registerManager.Register());
        _hostApplicationLifetime.ApplicationStopping.Register(() => _registerManager.DeRegister());

        return Task.CompletedTask;

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}