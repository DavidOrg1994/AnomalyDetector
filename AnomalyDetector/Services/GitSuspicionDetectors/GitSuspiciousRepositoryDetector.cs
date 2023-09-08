using AnomalyDetector.Contracts;
using AnomalyDetector.Options;
using AnomalyDetector.Services.NotiferService;
using Microsoft.Extensions.Options;

namespace AnomalyDetector.Services.GitSuspicionDetectors;

internal class GitSuspiciousRepositoryDetector : ISuspicionDetector
{
    private INotifier _notifier;
    private int _earlyDeleteThreshold;

    public GitSuspiciousRepositoryDetector(INotifier notifier, IOptions<RepositoryOptions> options)
    {
        _notifier = notifier;
        _earlyDeleteThreshold = options.Value.EarlyDelete;
    }

    public void Validate(WebhookRequest request)
    {
        DateTime.TryParse(request.Repository!.updated_at.ToString(), out DateTime updatedAt);
        DateTime.TryParse(request.Repository!.created_at.ToString(), out DateTime createdAt);
        if (IsDeleteTimeSuspicious(createdAt, updatedAt))
        {
            _notifier.Notify("There was a suspicious push operation");
        }
    }

    public bool IsDeleteTimeSuspicious(DateTime createdAt, DateTime updatedAt)
    {
        TimeSpan timeDifference = updatedAt - createdAt;
        return timeDifference <= TimeSpan.FromMinutes(_earlyDeleteThreshold);
    }
}
