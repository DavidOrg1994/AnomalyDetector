using AnomalyDetector.constants;
using AnomalyDetector.Contracts;
using AnomalyDetector.Services.WebhookHandlerService;
using Microsoft.AspNetCore.Mvc;

namespace AnomalyDetector.Controllers;

[ApiController]
[Route("[controller]")]
public class AnomalyDetectorController : ControllerBase
{
    private IWebhookHandler _webhookHandler;

    public AnomalyDetectorController(IWebhookHandler webhookHandler)
    {
        _webhookHandler = webhookHandler;
    }

    [HttpPost]
    public IActionResult HandleRepoCreateWebhook([FromBody] WebhookRequest request)
    {
        if (!Request.Headers.TryGetValue(General.OperationNameHeader, out var operationName))
        {
            return BadRequest($"{General.OperationNameHeader} header couldnt be found");
        }

        _webhookHandler.HandleWebhook(operationName!, request);
        return Ok();
    }
}