using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class WhatsAppCurrencyViewModel
{
    [JsonProperty("fallback_value")]
    public string FallBackValue { get; set; }
    [JsonProperty("code")]
    public string Code { get; set; } //The currency code as defined in ISO 4217. 
    [JsonProperty("amount_1000")] 
    public string AmountMultipliedBy1000 { get; set; }
}

