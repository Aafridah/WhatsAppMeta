using Newtonsoft.Json;

namespace WhatsAppMeta.Models;
public sealed class InitiateUploadResponseModel
{
    [JsonProperty("h")]
    public string HeaderHandle { get; set; }
}

