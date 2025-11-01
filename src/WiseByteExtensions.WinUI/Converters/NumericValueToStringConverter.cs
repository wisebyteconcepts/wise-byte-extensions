using Microsoft.UI.Xaml.Data;

using System;
using System.Globalization;

namespace WiseByteExtensions.WinUI.Converters;

/// <summary>
/// A value converter for WinUI that formats a numeric value (<see cref="decimal"/>, 
/// <see cref="double"/>, <see cref="float"/>, <see cref="int"/>, etc.) into a string 
/// using the format string specified in the converter parameter.
/// </summary>
/// <remarks>
/// The converter expects the <paramref name="parameter"/> to be a valid 
/// .NET numeric format string (for example: <c>"0.00"</c>, <c>"#,##0.00"</c>, <c>"C2"</c>).  
/// If the <paramref name="parameter"/> is <c>null</c> or invalid, the converter 
/// defaults to the format <c>"0.00"</c>.
///
/// Behavior summary:
/// <list type="bullet">
/// <item><description>If <paramref name="value"/> is <c>null</c>, returns <c>"null"</c>.</description></item>
/// <item><description>If <paramref name="value"/> is not numeric, returns <c>"NaN"</c>.</description></item>
/// <item><description>If <paramref name="value"/> is numeric, returns a formatted string using the specified or default format.</description></item>
/// </list>
///
/// Example usage in XAML:
/// <code>
/// &lt;TextBlock Text="{Binding Price, 
///                           Converter={StaticResource NumericValueToStringConverter}, 
///                           ConverterParameter='0.00'}" /&gt;
/// </code>
///
/// In this example:
/// - If <c>Price</c> = 123.456, the TextBlock will display "123.46".
/// - If <c>Price</c> = 0, the TextBlock will display "0.00".
/// - If <c>Price</c> = null, the TextBlock will display "null".
/// - If <c>Price</c> is not numeric, the TextBlock will display "NaN".
/// </remarks>
public partial class NumericValueToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is null)
            return "null";

        string format = parameter as string ?? "0.00";

        try
        {
            var culture = string.IsNullOrEmpty(language)
        ? CultureInfo.CurrentCulture
        : new CultureInfo(language);

            if (IsNumericType(value))
            {
                if (value is IFormattable formattable)
                    return formattable.ToString(format, culture);

                // fallback: force string.Format
                return string.Format(culture, "{0:" + format + "}", value);
            }

            return "NaN";
        }
        catch
        {
            // If formatting fails for any reason, treat as NaN
            return "NaN";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is not string stringValue)
            return 0;

        if (string.Equals(stringValue, "null", StringComparison.OrdinalIgnoreCase))
            return double.NaN;

        if (string.Equals(stringValue, "NaN", StringComparison.OrdinalIgnoreCase))
            return double.NaN;

        var culture = string.IsNullOrEmpty(language)
            ? CultureInfo.CurrentCulture
            : new CultureInfo(language);

        if (!double.TryParse(stringValue, NumberStyles.Any, culture, out double result))
            return 0;

        try
        {
            // Handle numeric conversions properly
            if (targetType == typeof(double))
                return result;

            if (targetType == typeof(float))
                return (float)result;

            if (targetType == typeof(decimal))
                return (decimal)result;

            if (targetType == typeof(int))
                return (int)Math.Round(result);

            if (targetType == typeof(long))
                return (long)Math.Round(result);

            if (targetType == typeof(object))
                return result; // safest fallback

            return System.Convert.ChangeType(result, targetType, culture);
        }
        catch
        {
            return 0;
        }
    }


    private static bool IsNumericType(object value)
    {
        return value is sbyte or byte or short or ushort or int or uint
            or long or ulong or float or double or decimal;
    }
}
