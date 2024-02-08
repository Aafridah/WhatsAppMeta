using WhatsAppMeta.Enums;

namespace WhatsAppMeta.Models;
public class MapItem
{
    public Type Type { get; private set; }
    public DataFetchType DataFetchType { get; private set; }
    public string PropertyName { get; private set; }

    public MapItem(Type type, DataFetchType dataFetchType, string propertyName)
    {
        Type = type;
        DataFetchType = dataFetchType;
        PropertyName = propertyName;
    }
}
