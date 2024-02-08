using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppParameterViewModel
{
    [JsonProperty("type")]
    public string Type { get; set; }//use enum : WhatsAppParameterType For text-based templates, the only supported parameter types are text, currency, and date_time
    [JsonProperty("text")]
    public string Text { get; set; } //For the header component, the character limit is 60 characters. For the body component, the character limit is 1024.
    [JsonProperty("currency")]
    public WhatsAppCurrencyViewModel Currency { get; set; } //Required when type is currency. 
    [JsonProperty("date_time")]
    public WhatsAppDateTimeViewModel DateTime { get; set; } //Required when type is date_time. 
    [JsonProperty("image")]
    public WhatsAppMediaViewModel Image { get; set; } //Required when type is image. 

    [JsonProperty("document")]
    public WhatsAppMediaViewModel Document { get; set; } //Required when type is document. 
}

