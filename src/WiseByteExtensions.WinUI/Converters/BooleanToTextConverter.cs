using Microsoft.UI.Xaml.Data;

using System;

namespace WiseByteExtensions.WinUI.Converters;
/// <summary>
/// A value converter for WinUI that maps a boolean value (true/false) to one of two strings 
/// specified in the converter parameter. This is useful for displaying different text in 
/// controls like <see cref="Microsoft.UI.Xaml.Controls.TextBlock"/> or any other content control 
/// based on a boolean property.
/// </summary>
/// <remarks>
/// The converter expects the <paramref name="parameter"/> to be a string containing two or more 
/// values separated by the '|' character:
/// 
/// <list type="bullet">
/// <item><description>The first value is returned when the bound value is <c>true</c>.</description></item>
/// <item><description>The second value is returned when the bound value is <c>false</c>.</description></item>
/// <item><description>If the parameter contains more than two values, only the first two are used.</description></item>
/// </list>
///
/// If the parameter is <c>null</c>, does not contain at least two parts, or the input value is 
/// not a <see cref="bool"/>, the converter returns the string "null".
///
/// Example usage with a TextBlock:
/// <code>
/// &lt;TextBlock Text="{Binding IsActive, Mode=OneWay, 
///                            Converter={StaticResource BooleanToStringByParameterConverter}, 
///                            ConverterParameter='Active|Inactive'}" /&gt;
/// </code>
///
/// In this example:
/// - If <c>IsActive</c> is <c>true</c>, the TextBlock will display "Active".
/// - If <c>IsActive</c> is <c>false</c>, the TextBlock will display "Inactive".
/// </remarks>
/// <param name="value">The input value, expected to be a <see cref="bool"/>.</param>
/// <param name="targetType">The target type (ignored).</param>
/// <param name="parameter">
/// A string containing two or more values separated by '|'.
/// The first value is returned for <c>true</c>, the second for <c>false</c>.
/// </param>
/// <param name="language">The culture/language (ignored).</param>
/// <returns>
/// The corresponding string for the boolean value, or "null" if the input or parameter is invalid.
/// </returns>
public partial class BooleanToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object? parameter, string language)
    {
        if (parameter is not string paramString)
            return "null";

        var parts = paramString.Split('|');
        if (parts.Length < 2)
            return "null";

        if (value is bool boolValue)
            return boolValue ? parts[0] : parts[1];

        return "null";
    }

    public object ConvertBack(object value, Type targetType, object? parameter, string language)
    {
        if (parameter is not string paramString)
            return false; // fallback if parameter is missing

        var parts = paramString.Split('|');
        if (parts.Length < 2)
            return false; // fallback if parameter format is invalid

        string stringValue = value?.ToString() ?? "";

        if (stringValue == parts[0])
            return true;
        if (stringValue == parts[1])
            return false;

        // If the string does not match either part, return a fallback (false)
        return false;
    }
}
