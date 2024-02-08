using Newtonsoft.Json;

namespace WhatsAppMeta.Models;
public sealed class WhatsAppDateTimeViewModel
{
    [JsonProperty("fallback_value")]
    public DateTime FallbackValue { get; set; }
    [JsonProperty("day_of_week")]
    public int? DayOfWeek { get; set; } //Supported values: "MONDAY" = 1, "TUESDAY" = 2, "WEDNESDAY" = 3, "THURSDAY" = 4, "FRIDAY" = 5, "SATURDAY" = 6, "SUNDAY" = 7
    [JsonProperty("year")]
    public int? Year { get; set; }
    [JsonProperty("month")]
    public int? Month { get; set; }
    [JsonProperty("day_of_month")]
    public int? DayOfMonth { get; set; }
    [JsonProperty("hour")]
    public int? Hour { get; set; }
    [JsonProperty("minute")]
    public int? Minute { get; set; }
    [JsonProperty("calendar")]
    public string Calendar { get; set; } //Values: "GREGORIAN" or "SOLAR_HIJRI"
}

