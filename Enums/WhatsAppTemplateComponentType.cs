using System.ComponentModel;

namespace WhatsAppMeta.Enums;
public enum WhatsAppTemplateComponentType
{
    [Description("HEADER")]
    Header = 1,
    [Description("BODY")]
    Body,
    [Description("FOOTER")]
    Footer,
    [Description("BUTTONS")]
    Button
}

