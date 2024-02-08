using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppTemplateViewModel
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("language")]
    public object Language { get; set; }
    [JsonProperty("components")]
    public IList<WhatsAppComponentViewModel> Components { get; set; }

}

