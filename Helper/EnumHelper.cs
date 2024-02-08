using System.ComponentModel;
using System.Reflection;

namespace WhatsAppMeta.Helper;

public static class EnumHelper
{
    /// <summary>
    ///     A generic extension method that aids in reflecting 
    ///     and retrieving any attribute that is applied to an `Enum`.
    /// </summary>
    // ReSharper disable once SuggestBaseTypeForParameter
    private static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DescriptionAttribute>()?
            .Description;
    }

    public static string GetDescription<T>(string value) where T : Enum
    {
        Enum enumItem = (T)Enum.Parse(typeof(T), value);
        var x = enumItem.GetDisplayName();
        return x ?? enumItem.ToString();
    }

    public static string GetEnumDescription(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());
        if (fi == null) return default;
        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }

    public static T GetValueFromDescription<T>(string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
            typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }

        throw new ArgumentException("Not found.", nameof(description));
    }
}