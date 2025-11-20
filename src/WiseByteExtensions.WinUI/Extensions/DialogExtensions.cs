using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryPro.UI.WinUI.Helpers;

public enum DialogSeverity
{
    None,
    Info,
    Success,
    Warning,
    Error,
    Question
}

public static class DialogExtensions
{
    // ---------------------------
    //  Internal helper
    // ---------------------------
    private static ContentDialog CreateBaseDialog(Window window)
    {
        if (window?.Content?.XamlRoot is null)
            throw new InvalidOperationException("Window/XamlRoot is not initialized.");

        return new ContentDialog
        {
            XamlRoot = window.Content.XamlRoot,
            RequestedTheme = (window.Content as FrameworkElement)?.RequestedTheme ?? ElementTheme.Default
        };
    }

    // ---------------------------
    //  Message Dialog (OK only)
    // ---------------------------
    public static async Task ShowMessageAsync(this Window window, string title, string message, string buttonText = "OK")
    {
        var dialog = CreateBaseDialog(window);
        dialog.Title = title;
        dialog.Content = message;
        dialog.CloseButtonText = buttonText;

        await dialog.ShowAsync();
    }

    // ---------------------------
    //  Confirmation (bool?)
    // ---------------------------
    public static async Task<bool?> ConfirmAsync(
        this Window window,
        string message,
        string title = "",
        string yesText = "OK",
        string noText = "Cancel")
    {
        var dialog = CreateBaseDialog(window);
        dialog.Title = title;
        dialog.Content = message;
        dialog.PrimaryButtonText = yesText;
        dialog.SecondaryButtonText = noText;
        dialog.DefaultButton = ContentDialogButton.Primary;

        var result = await dialog.ShowAsync();

        return result switch
        {
            ContentDialogResult.Primary => true,
            ContentDialogResult.Secondary => false,
            _ => (bool?)null
        };
    }

    // ---------------------------
    //  Confirmation (bool)
    // ---------------------------
    public static async Task<bool> ConfirmAsync(
        this Window window,
        string message,
        string title,
        string yesText)
    {
        var result = await window.ConfirmAsync(message, title, yesText, "Cancel");
        return result ?? false;
    }

    // ---------------------------
    //  Confirmation (with Cancel button)
    // ---------------------------
    public static async Task<bool?> ConfirmWithCancelAsync(
        this Window window,
        string message,
        string title = "",
        string yesText = "Yes",
        string noText = "No",
        string cancelText = "Cancel")
    {
        var dialog = CreateBaseDialog(window);
        dialog.Title = title;
        dialog.Content = message;

        dialog.PrimaryButtonText = yesText;
        dialog.SecondaryButtonText = noText;
        dialog.CloseButtonText = cancelText;
        dialog.DefaultButton = ContentDialogButton.Primary;

        var result = await dialog.ShowAsync();

        return result switch
        {
            ContentDialogResult.Primary => true,
            ContentDialogResult.Secondary => false,
            ContentDialogResult.None => null,
            _ => null
        };
    }


    // ---------------------------
    //  Input Text Dialog
    // ---------------------------
    public static async Task<string?> InputTextAsync(
        this Window window,
        string title,
        string message = "",
        string defaultText = "",
        string okText = "OK",
        string cancelText = "Cancel")
    {
        var stack = new StackPanel
        {
            Spacing = 10
        };

        if (!string.IsNullOrWhiteSpace(message))
        {
            stack.Children.Add(new TextBlock { Text = message });
        }

        var input = new TextBox
        {
            Text = defaultText,
            Height = 32,
            AcceptsReturn = false
        };

        stack.Children.Add(input);

        var dialog = CreateBaseDialog(window);
        dialog.Title = title;
        dialog.Content = stack;
        dialog.PrimaryButtonText = okText;
        dialog.SecondaryButtonText = cancelText;
        dialog.DefaultButton = ContentDialogButton.Primary;

        var result = await dialog.ShowAsync();
        return result == ContentDialogResult.Primary ? input.Text : null;
    }


    // ---------------------------
    //  Option Picker (non-generic)
    // ---------------------------
    public static async Task<object?> PickOptionAsync(
        this Window window,
        string title,
        IList<object> items,
        string okText = "OK",
        string cancelText = "Cancel",
        string displayPath = "")
    {
        var combo = new ComboBox
        {
            ItemsSource = items,
            DisplayMemberPath = displayPath,
            SelectedIndex = items.Count > 0 ? 0 : -1,
            Height = 32
        };

        var dialog = CreateBaseDialog(window);
        dialog.Title = title;
        dialog.Content = combo;
        dialog.PrimaryButtonText = okText;
        dialog.SecondaryButtonText = cancelText;
        dialog.DefaultButton = ContentDialogButton.Primary;

        var result = await dialog.ShowAsync();
        return result == ContentDialogResult.Primary ? combo.SelectedItem : null;
    }

    // ---------------------------
    //  Option Picker (generic)
    // ---------------------------
    public static async Task<T?> PickOptionAsync<T>(
        this Window window,
        string title,
        string message,
        IEnumerable<T> items,
        string okText = "OK",
        string cancelText = "Cancel",
        string displayPath = "")
    {
        var panel = new StackPanel { Spacing = 8 };

        panel.Children.Add(new TextBlock { Text = message });

        var combo = new ComboBox
        {
            ItemsSource = items,
            Height = 32,
            DisplayMemberPath = displayPath,
            SelectedIndex = items.Any() ? 0 : -1
        };

        panel.Children.Add(combo);

        var dialog = CreateBaseDialog(window);
        dialog.Title = title;
        dialog.Content = panel;
        dialog.PrimaryButtonText = okText;
        dialog.SecondaryButtonText = cancelText;

        var result = await dialog.ShowAsync();
        return result == ContentDialogResult.Primary ? (T?)combo.SelectedItem : default;
    }

    public static async Task ShowMessageAsync(
    this Window window,
    string title,
    string message,
    string buttonText = "OK",
    DialogSeverity severity = DialogSeverity.None)
    {
        var dialog = CreateBaseDialog(window);

        var panel = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };

        var icon = DialogVisuals.GetIcon(severity);
        if (icon != null)
            panel.Children.Add(icon);

        panel.Children.Add(new TextBlock
        {
            Text = message,
            TextWrapping = TextWrapping.Wrap
        });

        dialog.Title = title;
        dialog.Content = panel;
        dialog.CloseButtonText = buttonText;

        await dialog.ShowAsync();
    }

    public static async Task<bool?> ConfirmAsync(
    this Window window,
    string message,
    string title = "",
    string yesText = "OK",
    string noText = "Cancel",
    DialogSeverity severity = DialogSeverity.Question)
    {
        var dialog = CreateBaseDialog(window);

        var panel = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 12 };

        var icon = DialogVisuals.GetIcon(severity);
        if (icon != null)
            panel.Children.Add(icon);

        panel.Children.Add(new TextBlock { Text = message });

        dialog.Title = title;
        dialog.Content = panel;
        dialog.PrimaryButtonText = yesText;
        dialog.SecondaryButtonText = noText;
        dialog.DefaultButton = ContentDialogButton.Primary;

        var result = await dialog.ShowAsync();

        return result switch
        {
            ContentDialogResult.Primary => true,
            ContentDialogResult.Secondary => false,
            _ => (bool?)null
        };
    }

    public static async Task<string?> InputTextAsync(
    this Window window,
    string title,
    string message = "",
    string defaultText = "",
    string okText = "OK",
    string cancelText = "Cancel",
    DialogSeverity severity = DialogSeverity.None)
    {
        var dialog = CreateBaseDialog(window);

        var root = new StackPanel { Spacing = 12 };

        var header = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10 };
        var icon = DialogVisuals.GetIcon(severity);
        if (icon != null)
            header.Children.Add(icon);

        if (!string.IsNullOrWhiteSpace(message))
            header.Children.Add(new TextBlock { Text = message });

        root.Children.Add(header);

        var textBox = new TextBox
        {
            Text = defaultText,
            Height = 32
        };

        root.Children.Add(textBox);

        dialog.Title = title;
        dialog.Content = root;
        dialog.PrimaryButtonText = okText;
        dialog.SecondaryButtonText = cancelText;

        var result = await dialog.ShowAsync();

        return result == ContentDialogResult.Primary ? textBox.Text : null;
    }


    public static void ShowToast(
    this Window window,
    string message,
    DialogSeverity? severity = null,
    int durationMs = 2500)
    {
        var root = (window.Content as FrameworkElement);
        var rootGrid = window.EnsureRootGrid();

        if (root == null) return;

        var setSeverrity = severity ?? DialogSeverity.Info;

        var infoBar = new InfoBar
        {
            Message = message,
            IsOpen = true,
            Severity = setSeverrity switch
            {
                DialogSeverity.Info => InfoBarSeverity.Informational,
                DialogSeverity.Success => InfoBarSeverity.Success,
                DialogSeverity.Warning => InfoBarSeverity.Warning,
                DialogSeverity.Error => InfoBarSeverity.Error,
                _ => InfoBarSeverity.Informational
            },
            IsIconVisible = severity != null,
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 20),
        };



        rootGrid.Children.Add(infoBar);

        // Auto-close
        _ = Task.Run(async () =>
        {
            await Task.Delay(durationMs);

            window.DispatcherQueue.TryEnqueue(() =>
            {
                infoBar.IsOpen = false;
                rootGrid.Children.Remove(infoBar);
            });
        });
    }


    public static Grid EnsureRootGrid(this Window window)
    {
        if (window.Content is Grid g)
            return g;

        // Convert root content into a Grid
        var oldContent = window.Content as UIElement;
        var rootGrid = new Grid();

        window.Content = rootGrid;

        if (oldContent != null)
            rootGrid.Children.Add(oldContent);

        return rootGrid;
    }


}

public static class DialogVisuals
{
    public static UIElement GetIcon(DialogSeverity severity)
    {
        var icon = severity switch
        {
            DialogSeverity.Info => new SymbolIcon(Symbol.ContactInfo),
            DialogSeverity.Success => new SymbolIcon(Symbol.Accept),
            DialogSeverity.Warning => new SymbolIcon(Symbol.Forward),
            DialogSeverity.Error => new SymbolIcon(Symbol.Cancel),
            DialogSeverity.Question => new SymbolIcon(Symbol.Help),
            _ => null
        };

        if (icon != null)
        {
            icon.Foreground = severity switch
            {
                DialogSeverity.Error => new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Red),
                DialogSeverity.Warning => new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.DarkOrange),
                DialogSeverity.Success => new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Green),
                _ => new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.SteelBlue)
            };
        }

        return icon;
    }
}
