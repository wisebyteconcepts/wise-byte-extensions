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


}
