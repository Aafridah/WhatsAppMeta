namespace WhatsAppMeta.Exceptions;

public sealed class TcpPortNumberException : Exception
{
    public TcpPortNumberException(short tcpPortNumber) : base($"TCPPortNumber cannot be less than 1. Current Value : {tcpPortNumber}")
    {
            
    }
}