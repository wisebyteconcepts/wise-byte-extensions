using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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



}
