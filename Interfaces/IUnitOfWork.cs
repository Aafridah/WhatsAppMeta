namespace WhatsAppMeta.Interfaces;
public interface IUnitOfWork
{
    IWhatsAppRepository WhatsAppRepository { get; }
}

