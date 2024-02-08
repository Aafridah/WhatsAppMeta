using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppComponentViewModel
{
    [JsonProperty("type")]
    public string Type { get; set; } //Enum : WhatsAppComponentType
    [JsonProperty("parameters")]
    public IList<WhatsAppParameterViewModel> Parameters { get; set; }
    [JsonProperty("sub_type")]
    public string SubType { get; set; } //Enum : WhatsAppComponentSubType
    [JsonProperty("index")]
    public string Index { get; set; }

}

