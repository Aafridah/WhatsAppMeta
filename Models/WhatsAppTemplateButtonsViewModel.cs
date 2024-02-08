using Newtonsoft.Json;

namespace WhatsAppMeta.Models
{
    public sealed class WhatsAppTemplateButtonsViewModel
    {
        [JsonProperty("type")]
        public string Type { get; set; }//Enum : WhatsAppTemplateButtonType
        [JsonProperty("otp_type")]
        public string OtpType { get; set; }//Enum : WhatsAppTemplateOtpType
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("example")]
        public string Example { get; set; }
    }
}
