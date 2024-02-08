using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppSendMessageResponseModel
{
    [JsonProperty("messaging_product")]
    public string MessagingProduct { get; set; }
    [JsonProperty("contacts")]
    public IList<object> Contacts { get; set; }
    [JsonProperty("messages")]
    public IList<object> Messages { get; set; }
}

