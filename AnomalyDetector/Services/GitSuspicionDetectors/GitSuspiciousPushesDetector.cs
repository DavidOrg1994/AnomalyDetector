using AnomalyDetector.Contracts;
using AnomalyDetector.Options;
using AnomalyDetector.Services.NotiferService;
using Microsoft.Extensions.Options;

namespace AnomalyDetector.Services.GitSuspicionDetectors;

internal class GitSuspiciousPushesDetector : ISuspicionDetector
{
    private INotifier _notifier;
    private TimeSpan _startTime;
    private TimeSpan _endTime;

    public GitSuspiciousPushesDetector(INotifier notifier, IOptions<PushTimeOptions> options)
    {
        _notifier = notifier;
        _startTime = options.Value.From;
        _endTime = options.Value.To;
    }

    public void Validate(WebhookRequest request)
    {
        if (IsTimeInRange(Convert.ToInt32(request.Repository!.pushed_at.ToString())))
        {
            _notifier.Notify("There was a suspicious push operation");
        }
    }

    private bool IsTimeInRange(int utcTimestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(utcTimestamp);
        DateTimeOffset localTime = TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.Local);
        TimeSpan timeOfDay = localTime.TimeOfDay;

        return timeOfDay >= _startTime && timeOfDay <= _endTime;
    }
}
