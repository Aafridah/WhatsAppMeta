using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class MediaIdResponseModel
{
    [JsonProperty("id")]
    public string Id { get; set; }
}

