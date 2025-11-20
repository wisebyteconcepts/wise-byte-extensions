using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using InventoryPro.UI.WinUI.Helpers;

using Microsoft.UI.Xaml;

namespace ExtensionSamples.ViewModels;

public partial class ExtensionsViewModel : ObservableObject
{
    private readonly Window MainWindow = App.MainWindow;

    public ExtensionsViewModel()
    {
    }

    [RelayCommand]
    private async Task ShowMessageAsync()
    {
        await MainWindow.ShowMessageAsync(
            title: "Information",
            message: "This is a simple message dialog.",
            severity: DialogSeverity.Info
        );
    }


    [RelayCommand]
    private async Task ConfirmAsync()
    {
        var result = await MainWindow.ConfirmAsync(
            message: "Do you want to continue?",
            title: "Confirmation",
            yesText: "Yes",
            severity: DialogSeverity.Question
        );

        if (result == true)
        {
            // User clicked Yes
            this.MainWindow.ShowToast("Action confirmed!", DialogSeverity.Success);
        }
        else
        {
            this.MainWindow.ShowToast("Action cancelled", DialogSeverity.Warning);
        }
    }

    [RelayCommand]
    private async Task NullableConfirmAsync()
    {
        bool? result = await MainWindow.ConfirmAsync(
            message: "Delete this item?",
            title: "Delete",
            yesText: "Delete",
            noText: "Cancel",
            severity: DialogSeverity.Warning
        );

        if (result == true)
        {
            MainWindow.ShowToast("Item deleted.", DialogSeverity.Success);
        }
        else if (result == false)
        {
            MainWindow.ShowToast("Cancelled.", DialogSeverity.Info);
        }
        else
        {
            MainWindow.ShowToast("No selection.", DialogSeverity.Error);
        }
    }

    [RelayCommand]
    private async Task AskNameAsync()
    {
        string? name = await MainWindow.InputTextAsync(
            title: "Enter Name",
            message: "Please enter your name:",
            defaultText: "",
            severity: DialogSeverity.Info
        );

        if (!string.IsNullOrWhiteSpace(name))
        {
            MainWindow.ShowToast($"Hello, {name}!", DialogSeverity.Success);
        }
        else
        {
            MainWindow.ShowToast("No name entered.", DialogSeverity.Warning);
        }
    }


    [RelayCommand]
    private async Task PickColorAsync()
    {
        var items = new List<object> { "Red", "Green", "Blue" };

        var selected = await MainWindow.PickOptionAsync(
            title: "Choose Color",
            items: items,
            displayPath: "",
            okText: "Select",
            cancelText: "Cancel"
        );

        if (selected != null)
            MainWindow.ShowToast($"Selected: {selected}", DialogSeverity.Success);
        else
            MainWindow.ShowToast("Selection cancelled.", DialogSeverity.Warning);
    }

    [RelayCommand]

    private async Task PickCustomerAsync()
    {
        var customers = new List<string>
    {
         { "Alice" },
        {  "Bob" }
    };

        var selected = await MainWindow.PickOptionAsync(
            title: "Select Customer",
            message: "Choose a customer from the list",
            items: customers,
            displayPath: "Name",
            okText: "OK",
            cancelText: "Cancel"
        );

        if (selected is not null)
            MainWindow.ShowToast($"Customer: {selected}", DialogSeverity.Success);
        else
            MainWindow.ShowToast("No customer selected.", DialogSeverity.Info);
    }


    [RelayCommand]
    private void ShowToast()
    {
        MainWindow.ShowToast("This is a toast notification!");
    }

}
