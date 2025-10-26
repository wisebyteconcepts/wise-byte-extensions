using Microsoft.UI.Xaml.Data;

using System;

namespace WiseByteExtensions.WinUI.Converters;

/// <summary>
/// A value converter for WinUI that maps a three-state boolean value (<c>true</c>, <c>false</c>, or <c>null</c>) 
/// to corresponding text values and vice versa. This is useful for displaying textual representations 
/// of a boolean that may also be undefined, for example in controls like 
/// <see cref="Microsoft.UI.Xaml.Controls.TextBlock"/> or any content control that represents 
/// a tri-state property.
/// </summary>
/// <remarks>
/// The converter expects the <paramref name="parameter"/> to be a string containing three values 
/// separated by the '|' character:
/// 
/// <list type="bullet">
/// <item><description>The first value is returned when the bound value is <c>true</c>.</description></item>
/// <item><description>The second value is returned when the bound value is <c>false</c>.</description></item>
/// <item><description>The third value is returned when the bound value is <c>null</c>.</description></item>
/// </list>
///
/// The converter supports two-way binding:
/// <list type="bullet">
/// <item><description>Converts a boolean or null to a string using <see cref="Convert"/>.</description></item>
/// <item><description>Converts the string back to a boolean or null using <see cref="ConvertBack"/>.</description></item>
/// </list>
///
/// Example usage with a TextBlock:
/// <code>
/// &lt;TextBlock Text="{Binding ThreeStateValue, Mode=TwoWay, 
///                            Converter={StaticResource ThreeStateBooleanToTextConverter}, 
///                            ConverterParameter='Yes|No|Unknown'}" /&gt;
/// </code>
///
/// In this example:
/// - If <c>ThreeStateValue</c> is <c>true</c>, the TextBlock will display "Yes".
/// - If <c>ThreeStateValue</c> is <c>false</c>, the TextBlock will display "No".
/// - If <c>ThreeStateValue</c> is <c>null</c>, the TextBlock will display "Unknown".
/// </remarks>
/// <param name="value">The input value, expected to be a <see cref="bool"/> or <c>null</c>.</param>
/// <param name="targetType">The target type (ignored).</param>
/// <param name="parameter">
/// A string containing three values separated by '|'.
/// The first value is used for <c>true</c>, the second for <c>false</c>, and the third for <c>null</c>.
/// </param>
/// <param name="language">The culture/language (ignored).</param>
/// <returns>
/// The corresponding string for the boolean or null value, or "null" if the parameter is invalid.
/// </returns>

public partial class ThreeStateBooleanToTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, string language)
    {
        if (parameter is not string paramString)
            return "null";

        var parts = paramString.Split('|');
        if (parts.Length < 3)
            return "null";

        if (value is bool boolValue)
            return boolValue ? parts[0] : parts[1];

        // Treat null value
        return parts[2];
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, string language)
    {
        if (parameter is not string paramString)
            return null;

        var parts = paramString.Split('|');
        if (parts.Length < 3)
            return null;

        string stringValue = value?.ToString() ?? "";

        if (stringValue == parts[0])
            return true;
        if (stringValue == parts[1])
            return false;
        if (stringValue == parts[2])
            return null;

        return null; // fallback

    }
}
