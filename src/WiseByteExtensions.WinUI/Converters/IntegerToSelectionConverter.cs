using Microsoft.UI.Xaml.Data;

using System;

namespace WiseByteExtensions.WinUI.Converters;
/// <summary>
/// Converts between an integer value and a boolean selection state.
/// </summary>
/// <remarks>
/// This converter is useful when binding a numeric view-model property to UI elements that represent selection
/// (for example, a group of radio buttons where each radio button has a <c>CommandParameter</c> or similar
/// indicating an integer index). The converter returns <see langword="true"/> when the source integer equals
/// the integer provided by the converter <paramref name="parameter"/>. When converting back, if the target value
/// is <see langword="true"/>, the converter returns the integer represented by <paramref name="parameter"/>.
/// </remarks>
public class IntegerToSelectionConverter : IValueConverter
{
    /// <summary>
    /// Converts a source integer to a boolean selection state by comparing it to the supplied parameter.
    /// </summary>
    /// <param name="value">
    /// The source value. Expected to be an <see cref="int"/> or a string that can be parsed to an <see cref="int"/>.
    /// </param>
    /// <param name="targetType">The target binding type (unused).</param>
    /// <param name="parameter">
    /// The comparison value. Expected to be an <see cref="int"/> or a string that can be parsed to an <see cref="int"/>.
    /// Typically provided via XAML as the radio button's parameter.
    /// </param>
    /// <param name="language">The language/culture information (unused).</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> and <paramref name="parameter"/> parse to the same integer;
    /// otherwise <see langword="false"/>. If either <paramref name="value"/> or <paramref name="parameter"/> is <see langword="null"/>,
    /// returns <see langword="false"/>.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        // Check if the value is type of integer
        // Parse the parameter to a integer
        // If the value == parsed parameter integer then return true
        if (value == null || parameter == null)
            return false;

        // Try to parse both value and parameter as integers
        if (int.TryParse(value.ToString(), out int intValue) &&
            int.TryParse(parameter.ToString(), out int paramValue))
        {
            return intValue == paramValue;
        }

        return false;
    }

    /// <summary>
    /// Converts a boolean selection state back to an integer based on the supplied parameter.
    /// </summary>
    /// <param name="value">
    /// The target value. Expected to be a <see cref="bool"/> where <see langword="true"/> indicates selection.
    /// </param>
    /// <param name="targetType">The type to convert back to (unused).</param>
    /// <param name="parameter">
    /// The integer value to return when <paramref name="value"/> is <see langword="true"/>.
    /// Expected to be an <see cref="int"/> or a string parseable to <see cref="int"/>.
    /// </param>
    /// <param name="language">The language/culture information (unused).</param>
    /// <returns>
    /// The integer parsed from <paramref name="parameter"/> when <paramref name="value"/> is <see langword="true"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Thrown when <paramref name="value"/> is not a selected boolean or when no <paramref name="parameter"/> is provided.
    /// </exception>
    /// <remarks>
    /// Common usage: bind a radio button's <c>IsChecked</c> to a numeric view-model property using this converter,
    /// providing each radio button with a different integer <c>CommandParameter</c>. When a radio button becomes checked,
    /// this method returns that radio button's integer parameter.
    /// </remarks>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        // reverse based on paramater
        if (value is bool isSelected && isSelected && parameter != null)
        {
            if (int.TryParse(parameter.ToString(), out int paramValue))
            {
                return paramValue;
            }
        }

        throw new NotSupportedException();
    }
}
