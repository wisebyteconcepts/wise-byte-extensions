
using Microsoft.UI.Xaml.Data;

using System;

namespace WiseByteExtensions.WinUI.Converters;


/// <summary>
/// A value converter for WinUI that maps a boolean value (true/false) to a 
/// selectable state and vice versa. This converter is useful for controls 
/// like <see cref="Microsoft.UI.Xaml.Controls.RadioButton"/> or other selection 
/// controls where a boolean property determines which option is selected.
/// </summary>
/// <remarks>
/// This converter supports two-way binding between a non-nullable boolean property 
/// and a UI selection control. The <paramref name="parameter"/> defines which 
/// boolean value the converter should interpret:
/// 
/// <list type="bullet">
/// <item><description>"true" — returns true when the bound value is true.</description></item>
/// <item><description>"false" — returns true when the bound value is false.</description></item>
/// </list>
///
/// Example usage with RadioButtons:
/// <code>
/// &lt;StackPanel&gt;
///     &lt;RadioButton Content="True" 
///                  IsChecked="{Binding BooleanValue, Mode=TwoWay, 
///                              Converter={StaticResource BooleanToSelectionConverter}, 
///                              ConverterParameter='true'}" /&gt;
///     &lt;RadioButton Content="False" 
///                  IsChecked="{Binding BooleanValue, Mode=TwoWay, 
///                              Converter={StaticResource BooleanToSelectionConverter}, 
///                              ConverterParameter='false'}" /&gt;
/// &lt;/StackPanel&gt;
/// </code>
///
/// This setup allows the user to select True or False, and updates the underlying 
/// boolean property accordingly. Unlike ThreeStateBooleanToSelectionConverter, 
/// this converter does not support null/undefined values.
/// </remarks>
public partial class BooleanToSelectionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
        {
            if (parameter == null)
            {
                return true;
            }
            return false;
        }
        else if ((bool)value! == true && parameter != null && bool.TryParse(parameter.ToString(), out var boolValue) && boolValue == true)
        {
            return true;
        }
        else if ((bool)value! == false && parameter != null && bool.TryParse(parameter.ToString(), out var boolValue1) && boolValue1 == false)
        {
            return true;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isChecked)
        {
            if (parameter == null)
            {
                return false; // always false when parameter is null
            }

            if (bool.TryParse(parameter.ToString(), out bool paramValue))
            {
                return isChecked ? paramValue : !paramValue; // toggle based on isChecked
            }
        }

        return false;
    }
}
