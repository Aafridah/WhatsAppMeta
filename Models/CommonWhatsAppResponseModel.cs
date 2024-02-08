using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class CommonWhatsAppResponseModel
{
    [JsonProperty("success")]
    public bool Success { get; set; }
    [JsonProperty("error")]
    public object Error { get; set; }
}

