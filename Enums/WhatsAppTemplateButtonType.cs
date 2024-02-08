using System.ComponentModel;

namespace WhatsAppMeta.Enums;

public enum WhatsAppTemplateButtonType
{
    [Description("OTP")]
    Otp = 1,
    [Description("QUICK_REPLY")]
    QuickReply,
    [Description("PHONE_NUMBER")]
    PhoneNumber,
    [Description("URL")]
    Url
}

