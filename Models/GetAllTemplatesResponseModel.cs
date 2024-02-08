using Newtonsoft.Json;

namespace WhatsAppMeta.Models;

public sealed class GetAllTemplatesResponseModel
{
    [JsonProperty("data")]
    public IList<WhatsAppCreateTemplateRequestModel> Data { get; set; }
    [JsonProperty("paging")]
    public object Paging { get; set; }
}
