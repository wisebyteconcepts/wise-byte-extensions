using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.



namespace WiseByteExtensions.WinUI.Controls;
public sealed partial class Tag : UserControl
{

    private readonly static Brush successBackgroundColor = (Brush)Application.Current.Resources["SystemFillColorSuccessBackgroundBrush"];
    private readonly static Brush successTextColor = (Brush)Application.Current.Resources["SystemFillColorSuccessBrush"];

    private readonly static Brush warningBackgroundColor = (Brush)Application.Current.Resources["SystemFillColorCautionBackgroundBrush"];
    private readonly static Brush warningTextColor = (Brush)Application.Current.Resources["SystemFillColorCautionBrush"];

    private readonly static Brush criticalBackgroundColor = (Brush)Application.Current.Resources["SystemFillColorCriticalBackgroundBrush"];
    private readonly static Brush criticalTextColor = (Brush)Application.Current.Resources["SystemFillColorCriticalBrush"];

    private readonly static Brush defaultBackgroundColor = (Brush)Application.Current.Resources["AccentAcrylicBackgroundFillColorDefaultBrush"];
    private readonly static Brush defaultTextColor = (Brush)Application.Current.Resources["AccentTextFillColorSecondaryBrush"];

    public Tag()
    {
        this.InitializeComponent();
        DataContext = this;
    }


    private void ApplyFlagStyles()
    {
        switch (Flag)
        {
            case TagFlag.Success:
                BackgroundColor = successBackgroundColor;
                TextColor = successTextColor;
                PrefixColor = successTextColor;
                SuffixColor = successTextColor;
                IconColor = successTextColor;
                break;

            case TagFlag.Warning:
                BackgroundColor = warningBackgroundColor;
                TextColor = warningTextColor;
                PrefixColor = warningTextColor;
                SuffixColor = warningTextColor;
                IconColor = warningTextColor;
                break;

            case TagFlag.Critical:
                BackgroundColor = criticalBackgroundColor;
                TextColor = criticalTextColor;
                PrefixColor = criticalTextColor;
                SuffixColor = criticalTextColor;
                IconColor = criticalTextColor;
                break;

            case TagFlag.None:
                BackgroundColor = defaultBackgroundColor;
                TextColor = defaultTextColor;
                PrefixColor = defaultTextColor;
                SuffixColor = defaultTextColor;
                IconColor = defaultTextColor;
                break;


            default:
                BackgroundColor ??= defaultBackgroundColor;
                TextColor ??= defaultTextColor;
                PrefixColor ??= defaultTextColor;
                SuffixColor ??= defaultTextColor;
                IconColor ??= defaultTextColor;
                break;
        }
    }

    private void UpdateVisibility()
    {
        IconPresenter.Visibility = Icon != null ? Visibility.Visible : Visibility.Collapsed;
        PrefixText.Visibility = string.IsNullOrEmpty(Prefix) ? Visibility.Collapsed : Visibility.Visible;
        SuffixText.Visibility = string.IsNullOrEmpty(Suffix) ? Visibility.Collapsed : Visibility.Visible;
    }

    #region Dependency Properties

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(Tag), new PropertyMetadata("", OnElementChanged));

    public string Prefix
    {
        get => (string)GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }
    public static readonly DependencyProperty PrefixProperty =
        DependencyProperty.Register(nameof(Prefix), typeof(string), typeof(Tag), new PropertyMetadata("", OnElementChanged));

    public string Suffix
    {
        get => (string)GetValue(SuffixProperty);
        set => SetValue(SuffixProperty, value);
    }
    public static readonly DependencyProperty SuffixProperty =
        DependencyProperty.Register(nameof(Suffix), typeof(string), typeof(Tag), new PropertyMetadata("", OnElementChanged));

    public IconElement Icon
    {
        get => (IconElement)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(IconElement), typeof(Tag), new PropertyMetadata(null, OnElementChanged));

    public TagFlag Flag
    {
        get => (TagFlag)GetValue(FlagProperty);
        set => SetValue(FlagProperty, value);
    }
    public static readonly DependencyProperty FlagProperty =
        DependencyProperty.Register(nameof(Flag), typeof(TagFlag), typeof(Tag), new PropertyMetadata(TagFlag.None, OnFlagChanged));

    public int Spacing
    {
        get => (int)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }
    public static readonly DependencyProperty SpacingProperty =
        DependencyProperty.Register(nameof(Spacing), typeof(int), typeof(Tag), new PropertyMetadata(4));

    public int IconSize
    {
        get => (int)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(int), typeof(Tag), new PropertyMetadata(10));


    public Brush IconColor
    {
        get => (Brush)GetValue(IconColorProperty);
        set
        {
            SetValue(IconColorProperty, value);
        }
    }
    public static readonly DependencyProperty IconColorProperty =
        DependencyProperty.Register(nameof(IconColor), typeof(Brush), typeof(Tag), new PropertyMetadata(defaultTextColor));

    public Brush PrefixColor
    {
        get => (Brush)GetValue(PrefixColorProperty);
        set
        {
            SetValue(PrefixColorProperty, value);
        }
    }
    public static readonly DependencyProperty PrefixColorProperty =
        DependencyProperty.Register(nameof(PrefixColor), typeof(Brush), typeof(Tag), new PropertyMetadata(defaultTextColor));

    public Brush SuffixColor
    {
        get => (Brush)GetValue(SuffixColorProperty);
        set
        {
            SetValue(SuffixColorProperty, value);
        }
    }
    public static readonly DependencyProperty SuffixColorProperty =
        DependencyProperty.Register(nameof(SuffixColor), typeof(Brush), typeof(Tag), new PropertyMetadata(defaultTextColor));

    public Brush TextColor
    {
        get => (Brush)GetValue(TextColorProperty);
        set
        {
            SetValue(TextColorProperty, value);
        }
    }
    public static readonly DependencyProperty TextColorProperty =
        DependencyProperty.Register(nameof(TextColor), typeof(Brush), typeof(Tag), new PropertyMetadata(defaultTextColor));

    public Brush BackgroundColor
    {
        get => (Brush)GetValue(BackgroundColorProperty);
        set
        {
            SetValue(BackgroundColorProperty, value);
        }
    }
    public static readonly DependencyProperty BackgroundColorProperty =
        DependencyProperty.Register(nameof(BackgroundColor),
            typeof(Brush), typeof(Tag),
            new PropertyMetadata(defaultBackgroundColor));


    private static void OnElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Tag tag)
        {
            tag.UpdateVisibility();
        }
    }
    private static void OnFlagChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Tag tag)
        {
            tag.ApplyFlagStyles();
        }
    }

    #endregion
}

public enum TagFlag
{
    None,
    Success,
    Warning,
    Critical
}
