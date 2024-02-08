namespace WhatsAppMeta.Interfaces;
public interface IWhatsAppRepository
{
    Task<T> GetCustomerWhatsAppCredentialsByCompanyId<T>(CancellationToken token, int companyId);
}

