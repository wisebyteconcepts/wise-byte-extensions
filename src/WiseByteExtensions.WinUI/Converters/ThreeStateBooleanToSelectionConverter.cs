using Microsoft.UI.Xaml.Data;

using System;

namespace WiseByteExtensions.WinUI.Converters;

/// <summary>
/// A value converter for WinUI that maps a three-state boolean (true, false, null) 
/// to a selection state, and vice versa. This converter is useful for controls 
/// like <see cref="Microsoft.UI.Xaml.Controls.RadioButton"/> or any scenario 
/// where a nullable boolean needs to be represented as a selectable option.
/// </summary>
/// <remarks>
/// This converter supports two-way binding between a nullable boolean property 
/// and a UI representation of a selection state. The <paramref name="parameter"/> 
/// defines which boolean state the converter should evaluate:
/// 
/// <list type="bullet">
/// <item><description>"true" — maps to <c>true</c></description></item>
/// <item><description>"false" — maps to <c>false</c></description></item>
/// <item><description>"null" — maps to <c>null</c></description></item>
/// </list>
///
/// Example usage with RadioButtons:
/// <code>
/// &lt;StackPanel&gt;
///     &lt;RadioButton Content="True" 
///                  IsChecked="{Binding ThreeStateBoolean, Mode=TwoWay, 
///                              Converter={StaticResource ThreeStateBooleanToSelectionConverter}, 
///                              ConverterParameter='true'}" /&gt;
///     &lt;RadioButton Content="False" 
///                  IsChecked="{Binding ThreeStateBoolean, Mode=TwoWay, 
///                              Converter={StaticResource ThreeStateBooleanToSelectionConverter}, 
///                              ConverterParameter='false'}" /&gt;
///     &lt;RadioButton Content="Undefined" 
///                  IsChecked="{Binding ThreeStateBoolean, Mode=TwoWay, 
///                              Converter={StaticResource ThreeStateBooleanToSelectionConverter}, 
///                              ConverterParameter='null'}" /&gt;
/// &lt;/StackPanel&gt;
/// </code>
///
/// This setup allows the user to select True, False, or Undefined, and updates 
/// the underlying nullable boolean property accordingly.
/// </remarks>
public partial class ThreeStateBooleanToSelectionConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, string? language)
    {
        if (parameter is not string param)
            return value == null;

        if (param.Equals("true", StringComparison.OrdinalIgnoreCase))
            return value is bool b && b;

        if (param.Equals("false", StringComparison.OrdinalIgnoreCase))
            return value is bool b && !b;

        if (param.Equals("null", StringComparison.OrdinalIgnoreCase))
            return value is null;

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, string? language)
    {
        var param = parameter as string;

        if (value is bool isChecked)
        {
            if (param == null)
                return isChecked ? null : null;

            if (param.Equals("true", StringComparison.OrdinalIgnoreCase))
                return isChecked;
            if (param.Equals("false", StringComparison.OrdinalIgnoreCase))
                return !isChecked;
            if (param.Equals("null", StringComparison.OrdinalIgnoreCase))
                return null;
        }

        if (value == null && param == null)
            return true;

        return false;
    }
}
