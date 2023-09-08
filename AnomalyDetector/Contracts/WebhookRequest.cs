namespace AnomalyDetector.Contracts;

public class WebhookRequest
{
    public string Action { get; init; } = string.Empty;
    public RepositoryCreateRequest? Repository { get; set; }
    public Team? Team { get; set; }
}
