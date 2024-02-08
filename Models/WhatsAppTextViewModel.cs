using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppTextViewModel
{
    [JsonProperty("body")]
    public string Body { get; set; } //Maximum length: 4096 characters
    [JsonProperty("preview_url")]
    public bool PreviewUrl { get; set; }

}

