using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppTemplateComponents
{
    [JsonProperty("type")]
    public string Type { get; set; } //Enum : WhatsAppTemplateComponentType
    [JsonProperty("format")]
    public string Format { get; set; }//Enum :  WhatsAppTemplateComponentFormat
    [JsonProperty("text")]
    public string Text { get; set; }
    [JsonProperty("example")]
    public object Example { get; set; }
    [JsonProperty("buttons")]
    public IList<WhatsAppTemplateButtonsViewModel> Buttons { get; set; }

}

