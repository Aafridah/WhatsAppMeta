using Newtonsoft.Json;

namespace WhatsAppMeta.Models;
public sealed class WhatsAppCallBackRequest
{
    [JsonProperty("field")]
    public string Field { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }
}

