using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

using System;

namespace WiseByteExtensions.WinUI.Converters;

/// <summary>
/// A value converter for WinUI that maps a boolean value (<c>true</c>/<c>false</c>) 
/// to a <see cref="Microsoft.UI.Xaml.Visibility"/> value.  
/// This converter is typically used to show or hide UI elements based on a boolean property.
/// </summary>
/// <remarks>
/// By default:
/// <list type="bullet">
/// <item><description><c>true</c> → <see cref="Microsoft.UI.Xaml.Visibility.Visible"/></description></item>
/// <item><description><c>false</c> → <see cref="Microsoft.UI.Xaml.Visibility.Collapsed"/></description></item>
/// </list>
///
/// The conversion logic can be inverted by passing the string parameter <c>"true"</c> 
/// in the converter parameter.  
/// When the parameter is <c>"true"</c>:
/// <list type="bullet">
/// <item><description><c>true</c> → <see cref="Microsoft.UI.Xaml.Visibility.Collapsed"/></description></item>
/// <item><description><c>false</c> → <see cref="Microsoft.UI.Xaml.Visibility.Visible"/></description></item>
/// </list>
///
/// Example usage in XAML:
/// <code>
/// &lt;Page.Resources&gt;
///     &lt;converters:BooleanToVisibilityConverter x:Key="BoolToVisibility" /&gt;
/// &lt;/Page.Resources&gt;
///
/// &lt;!-- Normal conversion: true = Visible, false = Collapsed --&gt;
/// &lt;TextBlock Text="Active"
///            Visibility="{Binding IsActive, 
///                         Converter={StaticResource BoolToVisibility}}" /&gt;
///
/// &lt;!-- Inverted conversion: true = Collapsed, false = Visible --&gt;
/// &lt;TextBlock Text="Inactive"
///            Visibility="{Binding IsActive, 
///                         Converter={StaticResource BoolToVisibility}, 
///                         ConverterParameter=true}" /&gt;
/// </code>
/// </remarks>
/// <param name="value">The input value, expected to be a <see cref="bool"/>.</param>
/// <param name="targetType">The target type (ignored).</param>
/// <param name="parameter">
/// Optional string parameter that determines inversion behavior.  
/// If set to <c>"true"</c>, the visibility mapping is reversed.
/// </param>
/// <param name="language">The culture or language (ignored).</param>
/// <returns>
/// <see cref="Microsoft.UI.Xaml.Visibility.Visible"/> when the value is <c>true</c>,
/// or <see cref="Microsoft.UI.Xaml.Visibility.Collapsed"/> when <c>false</c>.  
/// Returns inverted visibility when <paramref name="parameter"/> is <c>"true"</c>.
/// </returns>
public partial class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not bool boolValue)
            return Visibility.Collapsed;

        bool invert = parameter is string str && str.Equals("true", StringComparison.OrdinalIgnoreCase);

        if (invert)
            boolValue = !boolValue;

        return boolValue ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is not Visibility visibility)
            return false;

        bool result = visibility == Visibility.Visible;

        bool invert = parameter is string str && str.Equals("true", StringComparison.OrdinalIgnoreCase);

        return invert ? !result : result;
    }
}
