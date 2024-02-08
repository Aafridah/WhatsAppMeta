namespace WhatsAppMeta.Models;
public class CustomerWhatsAppCredentials
{
    public int CustomerWhatsAppCredentialId { get; set; }
    public int CustomerWhatsAppConnectionId { get; set; }
    public string WhatsAppNo { get; set; }
    public string PhoneNumberId { get; set; }
    public string WabaId { get; set; }
    public string AppId { get; set; }
    public string BusinessId { get; set; }
    public string FbApiKey { get; set; }
    public bool Deleted { get; set; }
    public short CreatedBy { get; set; }
    public DateTime CreatedDateTime { get; set; }
}
