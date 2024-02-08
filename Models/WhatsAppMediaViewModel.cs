using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppMediaViewModel
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("link")]
    public string Link { get; set; }
    [JsonProperty("caption")]
    public string Caption { get; set; } //Do not use it with audio or sticker media.
    [JsonProperty("filename")]
    public string FileName { get; set; } //Use only with document media.
}

