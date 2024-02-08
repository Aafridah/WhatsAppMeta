using System.ComponentModel;

namespace WhatsAppMeta.Enums;

public enum WhatsAppTemplateComponentFormat
{
    [Description("TEXT")]
    Text = 1,
    [Description("IMAGE")]
    Image,
    [Description("LOCATION")]
    Location,
    [Description("DOCUMENT")]
    Document,
 
}

