using Newtonsoft.Json;
using WhatsAppMeta.Enums;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppSendMessageRequestModel
{
    [JsonProperty("messaging_product")]
    public string MessagingProduct { get; set; }
    [JsonProperty("recipient_type")]
    public string RecipientType { get; set; }
    [JsonProperty("to")]
    public string To { get; set; }
    [JsonProperty("type")]
    public WhatsAppSendMessageType Type { get; set; }
    [JsonProperty("context")]
    public object Context { get; set; } //contains message id of prev msg when replying to a message
    [JsonProperty("template")]
    public WhatsAppTemplateViewModel Template { get; set; }
    
}

