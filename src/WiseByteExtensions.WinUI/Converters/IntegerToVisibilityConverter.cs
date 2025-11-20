using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

using System;

namespace WiseByteExtensions.WinUI.Converters;

/// <summary>
/// Converts an integer value to a <see cref="Visibility"/> state based on equality comparison with a parameter.
/// </summary>
/// <remarks>
/// This converter compares an integer value against a parameter value. If they are equal, it returns
/// <see cref="Visibility.Visible"/>; otherwise, it returns <see cref="Visibility.Collapsed"/>.
/// </remarks>
public class IntegerToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Converts an integer value to a <see cref="Visibility"/> state.
    /// </summary>
    /// <param name="value">The integer value to convert. Must be parseable as an <see cref="int"/>.</param>
    /// <param name="targetType">The type of the binding target property. This parameter is not used.</param>
    /// <param name="parameter">The comparison value as an integer. Must be parseable as an <see cref="int"/>.</param>
    /// <param name="language">The culture of the conversion. This parameter is not used.</param>
    /// <returns>
    /// <see cref="Visibility.Visible"/> if <paramref name="value"/> equals <paramref name="parameter"/>,;
    /// otherwise, <see cref="Visibility.Collapsed"/>.
    /// Returns <see cref="Visibility.Collapsed"/> if either <paramref name="value"/> or <paramref name="parameter"/> is <c>null</c>
    /// or cannot be parsed as an integer.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null || parameter == null || parameter is not string paramstring)
            return Visibility.Collapsed;

        if (!int.TryParse(value.ToString(), out int intValue))
            return Visibility.Collapsed;

        // Split multiple parameters using '|'
        var parameters = paramstring
            .Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        // Check if any parameter matches the value
        foreach (var param in parameters)
        {
            if (int.TryParse(param, out int paramValue) && intValue == paramValue)
                return Visibility.Visible;
        }

        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}
