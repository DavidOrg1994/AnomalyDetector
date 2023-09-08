namespace AnomalyDetector.Services.NotiferService;

internal class Notifier : INotifier
{
    private ILogger<Notifier> _logger;

    public Notifier(ILogger<Notifier> logger)
    {
        _logger = logger;
    }

    public void Notify(string message)
    {
        _logger.LogWarning(message);
    }
}
