using ExtensionSamples.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ExtensionSamples.Views;

public sealed partial class BlankPage : Page
{
    public BlankViewModel ViewModel
    {
        get;
    }

    public BlankPage()
    {
        ViewModel = App.GetService<BlankViewModel>();
        InitializeComponent();
    }
}
