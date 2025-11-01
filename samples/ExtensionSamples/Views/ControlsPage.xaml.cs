using ExtensionSamples.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ExtensionSamples.Views;

public sealed partial class ControlsPage : Page
{
    public ControlsViewModel ViewModel
    {
        get;
    }

    public ControlsPage()
    {
        ViewModel = App.GetService<ControlsViewModel>();
        InitializeComponent();
        DataContext = ViewModel;
    }
}
