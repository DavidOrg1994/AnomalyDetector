using AnomalyDetector.Contracts;
namespace AnomalyDetector.Services.GitSuspicionDetectors;

public interface ISuspicionDetector
{
    void Validate(WebhookRequest request);
}