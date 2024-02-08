using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppCreateTemplateResponseModel
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("category")]
    public string Category { get; set; }
}

