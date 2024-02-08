using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppCreateTemplateRequestModel
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }
    [JsonProperty("components")]
    public IList<WhatsAppTemplateComponents> Components { get; set; }

}

