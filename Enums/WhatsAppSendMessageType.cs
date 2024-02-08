using System.ComponentModel;

namespace WhatsAppMeta.Enums;
public enum WhatsAppSendMessageType : byte
{
    [Description("text")]
    Text = 1,
    [Description("template")]
    Template = 2,
    [Description("document")]
    Document = 3,
    [Description("image")]
    Image = 4,
    [Description("interactive")]
    Interactive = 5,
    [Description("audio")]
    Audio = 6,
    [Description("contacts")]
    Contacts = 7,
    [Description("location")]
    Location = 8,
    [Description("sticker")]
    Sticker = 9,
    [Description("video")]
    Video = 10,
}
