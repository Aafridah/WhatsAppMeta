using System.Data;
using WhatsAppMeta.Interfaces;
using WhatsAppMeta.Models;

namespace WhatsAppMeta.Repository;
public class WhatsAppRepository : IWhatsAppRepository
{
    private readonly IRepository _dbRepository;
    private readonly Logger<WhatsAppRepository> _logger;

    public WhatsAppRepository(IRepository dbRepository, Logger<WhatsAppRepository> logger)
    {
        _dbRepository = dbRepository;
        _logger = logger;
    }
    public async Task<T> GetCustomerWhatsAppCredentialsByCompanyId<T>(CancellationToken token, int companyId)
    {
        try
        {
            var parameters = new List<ParametersCollection>
            {
                new() {ParameterName = "@CompanyId", ParameterValue = companyId, ParameterType = DbType.Int32, ParameterDirection = ParameterDirection.Input}
            };
            return await _dbRepository.ExecuteSpSingleAsync<T>(token, "GetCustomerWhatsAppCredentialsByCompanyId", parameters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Executing Procedure GetCustomerWhatsAppCredentialsByCompanyId");
            throw;
        }
    }
}

