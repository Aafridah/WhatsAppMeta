using System.ComponentModel;

namespace WhatsAppMeta.Enums;
public enum WhatsAppComponentType
{
    [Description("header")]
    Header = 1,
    [Description("body")]
    Body,
    [Description("button")]
    Button
}

