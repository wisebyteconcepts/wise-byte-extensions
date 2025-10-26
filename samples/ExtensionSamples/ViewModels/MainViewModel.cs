using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ExtensionSamples.ViewModels;

public partial class MainViewModel : ObservableRecipient
{

    [ObservableProperty]
    private bool twoStateBoolean;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ThreeStateBooleanText))]
    private bool? threeStateBoolean;
    public string ThreeStateBooleanText => ThreeStateBoolean.HasValue ? ThreeStateBoolean.Value.ToString() : "null";


    [ObservableProperty]
    private bool twoStateBooleanForText;

    [ObservableProperty]
    private bool? threeStateBooleanForText;




    public MainViewModel()
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

}
