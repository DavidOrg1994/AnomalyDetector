using AnomalyDetector.Contracts;
using AnomalyDetector.Options;
using AnomalyDetector.Services.NotiferService;
using Microsoft.Extensions.Options;

namespace AnomalyDetector.Services.GitSuspicionDetectors;

internal class GitSuspiciousTeamsDetector : ISuspicionDetector
{
    private INotifier _notifier;
    private string _suspiciousPrefix;


    public GitSuspiciousTeamsDetector(INotifier notifier, IOptions<TeamOptions> options)
    {
        _notifier = notifier;
        _suspiciousPrefix = options.Value.SuspiciousName;
    }

    public void Validate(WebhookRequest request)
    {
        if (isStartingWithSuspiciousPrefix(request.Team!.name))
        {
            _notifier.Notify("Suspicious team name detected");
        }
    }

    public bool isStartingWithSuspiciousPrefix(string teamName)
    {
        return teamName.StartsWith(_suspiciousPrefix);
    }
}
