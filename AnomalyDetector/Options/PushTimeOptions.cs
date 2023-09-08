namespace AnomalyDetector.Options;

public class PushTimeOptions
{
    public required TimeSpan From { get; init; }
    public required TimeSpan To { get; init; }
}
