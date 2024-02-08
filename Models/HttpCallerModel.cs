using WhatsAppMeta.Enums;

namespace WhatsAppMeta.Models;
public class HttpCallerModel<T> where T : class
{
    public string Url { get; set; }
    public HttpRequestType Method { get; set; }
    public T Body { get; set; }
    public ContentType? ContentType { get; set; }
    public byte TimeOut { get; set; }
    public List<HttpHeadersModel> Headers { get; set; }
    public HttpAuthenticationType? AuthenticationType { get; set; }
    public string AuthKey { get; set; }
    public string AuthValue { get; set; }
    public bool RemoteCertificateValidationCallback { get; set; }
}