using System.Globalization;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Newtonsoft.Json.Linq;

namespace ExtensionSamples.ViewModels;

public partial class ConvertersViewModel : ObservableRecipient
{

    [ObservableProperty]
    private bool twoStateBoolean;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ThreeStateBooleanText))]
    private bool? threeStateBoolean;
    public string ThreeStateBooleanText => ThreeStateBoolean.HasValue ? ThreeStateBoolean.Value.ToString() : "null";


    [ObservableProperty]
    private bool twoStateBooleanForText, twoStateBooleanForBrush;

    [ObservableProperty]
    private bool? threeStateBooleanForText, threeStateBooleanForBrush;



    [ObservableProperty]
    public decimal numericValue;

    [ObservableProperty]
    private string numericValueText = string.Empty;


    public ConvertersViewModel()
    {
    }

    [RelayCommand]
    private async Task CheckThreeStateBoolean()
    {
        var state = ThreeStateBoolean.HasValue ? ThreeStateBoolean.Value.ToString() : "null";
        await App.MainWindow.ShowMessageDialogAsync($"ThreeStateBoolean is {state}");
    }

    [RelayCommand]
    private async Task CheckTwoStateBoolean()
    {
        await App.MainWindow.ShowMessageDialogAsync($"TwoStateBoolean is {TwoStateBoolean}");
    }

    [RelayCommand]
    private void TwoStateBooleanSwitcherForText(string switchBool)
    {
        if (string.Equals(switchBool, "true", StringComparison.OrdinalIgnoreCase))
        {
            TwoStateBooleanForText = true;
        }
        else if (string.Equals(switchBool, "false", StringComparison.OrdinalIgnoreCase))
        {
            TwoStateBooleanForText = false;
        }
    }


    [RelayCommand]
    private void ThreeStateBooleanSwitcherForText(string switchBool)
    {
        if (string.Equals(switchBool, "true", StringComparison.OrdinalIgnoreCase))
        {
            ThreeStateBooleanForText = true;
        }
        else if (string.Equals(switchBool, "false", StringComparison.OrdinalIgnoreCase))
        {
            ThreeStateBooleanForText = false;
        }
        else if (string.Equals(switchBool, "null", StringComparison.OrdinalIgnoreCase))
        {
            ThreeStateBooleanForText = null;
        }
    }


    [RelayCommand]
    private void TwoStateBooleanSwitcherForBrush(string switchBool)
    {
        if (string.Equals(switchBool, "true", StringComparison.OrdinalIgnoreCase))
        {
            TwoStateBooleanForBrush = true;
        }
        else if (string.Equals(switchBool, "false", StringComparison.OrdinalIgnoreCase))
        {
            TwoStateBooleanForBrush = false;
        }
    }


    [RelayCommand]
    private void ThreeStateBooleanSwitcherForBrush(string switchBool)
    {
        if (string.Equals(switchBool, "true", StringComparison.OrdinalIgnoreCase))
        {
            ThreeStateBooleanForBrush = true;
        }
        else if (string.Equals(switchBool, "false", StringComparison.OrdinalIgnoreCase))
        {
            ThreeStateBooleanForBrush = false;
        }
        else if (string.Equals(switchBool, "null", StringComparison.OrdinalIgnoreCase))
        {
            ThreeStateBooleanForBrush = null;
        }
    }


    private static decimal ParseDynamicNumericValue(string input, string? language = null)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0;

        var culture = string.IsNullOrEmpty(language)
            ? CultureInfo.CurrentCulture
            : new CultureInfo(language);

        input = input.Trim();

        // Try parsing in order of specificity
        if (int.TryParse(input, NumberStyles.Integer, culture, out var intValue))
            return intValue;

        if (long.TryParse(input, NumberStyles.Integer, culture, out var longValue))
            return longValue;

        if (decimal.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, culture, out var decimalValue))
            return decimalValue;

        return 0; // If nothing fits
    }


    partial void OnNumericValueTextChanged(string value)
    {
        NumericValue = ParseDynamicNumericValue(NumericValueText);
    }

}
