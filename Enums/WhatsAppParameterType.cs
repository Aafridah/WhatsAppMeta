using System.ComponentModel;

namespace WhatsAppMeta.Enums;
public enum WhatsAppParameterType
{
    [Description("text")]
    Text = 1,
    [Description("currency")]
    Currency = 2,
    [Description("date_time")]
    DateTime = 3,
    [Description("image")]
    Image = 4,
    [Description("document")]
    Document = 5,
}

