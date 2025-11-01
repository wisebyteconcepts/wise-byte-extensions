using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

using System;

namespace WiseByteExtensions.WinUI.Converters;

/// <summary>
/// A value converter for WinUI that maps a three-state boolean value (<c>true</c>, <c>false</c>, or <c>null</c>)
/// to a corresponding <see cref="Brush"/> resource.
/// </summary>
/// <remarks>
/// The <paramref name="parameter"/> must be a string containing three brush resource keys separated by the '|' character:
/// 
/// <list type="bullet">
/// <item><description>The first key is used when the bound value is <c>true</c>.</description></item>
/// <item><description>The second key is used when the bound value is <c>false</c>.</description></item>
/// <item><description>The third key is used when the bound value is <c>null</c>.</description></item>
/// </list>
///
/// Example usage:
/// <code>
/// &lt;Border Background="{Binding IsEnabled, 
///                               Converter={StaticResource ThreeStateBooleanToResourceBrushConverter},
///                               ConverterParameter='EnabledBrush|DisabledBrush|UnknownBrush'}" /&gt;
/// </code>
/// </remarks>
public partial class ThreeStateBooleanToResourceBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Fallback: transparent brush
        static Brush Transparent() => new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));

        if (parameter is not string paramString)
            return Transparent();

        var parts = paramString.Split('|');
        if (parts.Length < 3)
            return Transparent();

        string resourceKey = value switch
        {
            true => parts[0],
            false => parts[1],
            null => parts[2],
            _ => parts[2]
        };

        // Attempt to retrieve the brush resource
        if (Application.Current.Resources.TryGetValue(resourceKey, out var resource) && resource is Brush brush)
            return brush;

        // Resource not found → return transparent
        return Transparent();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
