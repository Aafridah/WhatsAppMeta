using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppButtonParameterViewModel
{
    [JsonProperty("type")]
    public string Type { get; set; } //Enum : WhatsAppButtonParameterType
    [JsonProperty("payload")]
    public string Payload { get; set; }
    [JsonProperty("text")]
    public string Text { get; set; }

}

