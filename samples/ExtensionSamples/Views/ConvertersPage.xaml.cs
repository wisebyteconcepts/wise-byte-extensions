using ExtensionSamples.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ExtensionSamples.Views;

public sealed partial class ConvertersPage : Page
{
    public ConvertersViewModel ViewModel
    {
        get;
    }

    public ConvertersPage()
    {
        ViewModel = App.GetService<ConvertersViewModel>();
        InitializeComponent();
        DataContext = ViewModel;
    }
}
