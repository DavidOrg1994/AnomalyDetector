using AnomalyDetector.Contracts;
using AnomalyDetector.Services.ValidationsFactory;

namespace AnomalyDetector.Services.WebhookHandlerService;

public class WebhookHandler : IWebhookHandler
{
    private IValidationFactory _validatorsFactory;
    private ILogger<WebhookHandler> _logger;

    public WebhookHandler(IValidationFactory validatorsFactory, ILogger<WebhookHandler> logger)
    {
        _validatorsFactory = validatorsFactory;
        _logger = logger;
    }

    public void HandleWebhook(string operationName, WebhookRequest request)
    {
        string key = BuildKey(operationName, request);
        var validator = _validatorsFactory.GetValidator(key);
        if (validator is null)
        {
            _logger.LogInformation($"No validator found for ${key}.");
            return;
        }

        validator.Validate(request);
    }

    private string BuildKey(string operationName, WebhookRequest request)
    {
        return string.IsNullOrEmpty(request.Action) ? operationName! : $"{operationName}_{request.Action}";
    }
}
