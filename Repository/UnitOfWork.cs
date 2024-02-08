using WhatsAppMeta.Interfaces;

namespace WhatsAppMeta.Repository;
public class UnitOfWork : IUnitOfWork
{
    //Define Data Access Repositories Here
    private readonly IRepository _dbRepository;
    private readonly Logger<WhatsAppRepository> _logger;

    public UnitOfWork(IRepository dbRepository, Logger<WhatsAppRepository> logger)
    {
        _dbRepository = dbRepository;
        _logger = logger;
    }
    private IWhatsAppRepository _whatsAppRepository;
    public IWhatsAppRepository WhatsAppRepository => _whatsAppRepository ??= new WhatsAppRepository(_dbRepository, _logger);
}
