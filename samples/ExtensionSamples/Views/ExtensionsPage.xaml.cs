using ExtensionSamples.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ExtensionSamples.Views;

public sealed partial class ExtensionsPage : Page
{
    public ExtensionsViewModel ViewModel
    {
        get;
    }

    public ExtensionsPage()
    {
        ViewModel = App.GetService<ExtensionsViewModel>();
        InitializeComponent();
        DataContext = ViewModel;
    }
}
