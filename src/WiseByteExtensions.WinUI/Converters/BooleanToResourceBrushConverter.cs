using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

using System;

using Windows.UI;

namespace WiseByteExtensions.WinUI.Converters;

/// <summary>
/// A value converter that maps a boolean value to a Brush from the application's resources.
/// </summary>
/// <remarks>
/// The converter expects the <paramref name="parameter"/> to be a string containing two resource keys 
/// separated by the '|' character:
/// <list type="bullet">
/// <item><description>The first key is used when the bound value is <c>true</c>.</description></item>
/// <item><description>The second key is used when the bound value is <c>false</c>.</description></item>
/// </list>
/// If the resource is not found or the parameter/value is invalid, a transparent brush is returned.
///
/// Example usage:
/// <code>
/// &lt;TextBlock Text="Status"
///            Foreground="{Binding IsActive,
///                                 Converter={StaticResource BooleanToResourceBrushConverter},
///                                 ConverterParameter='ActiveBrush|InactiveBrush'}" /&gt;
/// </code>
/// </remarks>
public partial class BooleanToResourceBrushConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Ensure parameter is a string like "TrueKey|FalseKey"
        if (parameter is not string paramString)
            return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        var parts = paramString.Split('|');
        if (parts.Length < 2)
            return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        if (value is not bool boolValue)
            return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        string resourceKey = boolValue ? parts[0] : parts[1];

        // Attempt to get the brush from Application.Resources
        if (Application.Current.Resources.TryGetValue(resourceKey, out var resource) && resource is Brush brush)
        {
            return brush;
        }

        // Resource not found → return transparent
        return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
