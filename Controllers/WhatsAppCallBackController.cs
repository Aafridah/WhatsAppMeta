using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatsAppMeta.Models;

namespace WhatsAppMeta.Controllers;
[ApiController]
[Route("[controller]")]
public class WhatsAppCallBackController : ControllerBase
{

    private readonly ILogger<WhatsAppCallBackController> _logger;
    private readonly IConfiguration _config;

    public WhatsAppCallBackController(ILogger<WhatsAppCallBackController> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ObjectResult> Get(CancellationToken token)
    {
        try
        {
            if (string.IsNullOrEmpty(Request.Query["hub.verify_token"])) return StatusCode(400, default);
            await Task.CompletedTask;
            return Request.Query["hub.verify_token"].ToString() != _config.GetSection("ApiConfiguration:WhatsAppCallBackVerifyToken").ToString() ? StatusCode(401, default) :
                StatusCode(200, Request.Query["hub.challenge"].ToString());

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in WhatsApp Callback Get.");
        }
        return StatusCode(200, Request.Query["hub.challenge"].ToString());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<StatusCodeResult> Post(CancellationToken token, WhatsAppCallBackRequest whatsAppCallBackRequest)
    {
        try
        {
            switch (whatsAppCallBackRequest.Field)
            {
                case "message_template_status_update":
                    //TODO update template status in db
                    break;
                case "messages":
                    //TODO update edr 
                    break;
                case "template_category_update":
                    //TODO update template category in db
                    break;
                case "campaign_status_update": break;
                case "flows": break;
                case "account_alerts": break;
                case "account_review_update": break;
                case "account_update": break;
                case "business_capability_update": break;
                case "business_status_update": break;
                case "message_echoes": break;
                case "message_template_quality_update": break;
                case "messaging_handovers": break;
                case "phone_number_name_update": break;
                case "phone_number_quality_update": break;
                case "security": break;
            }
            return StatusCode(200);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in WhatsApp Callback Post.");
        }
        return StatusCode(200);
    }

}

