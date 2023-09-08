using AnomalyDetector.Contracts;
namespace AnomalyDetector.Services.WebhookHandlerService;

public interface IWebhookHandler
{
    void HandleWebhook(string operationName, WebhookRequest request);
}