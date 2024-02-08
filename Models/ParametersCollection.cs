using System.Data;

namespace WhatsAppMeta.Models;
public class ParametersCollection
{
    public string ParameterName { get; set; } = null!;
    public object? ParameterValue { get; set; }
    public DbType ParameterType { get; set; }
    public ParameterDirection ParameterDirection { get; set; }
}
