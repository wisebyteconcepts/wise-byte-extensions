using Microsoft.UI.Xaml;

using WiseByteExtensions.WinUI.Converters;

using Xunit;

namespace WiseByteExtensions.Tests.Converters;
public class BooleanToVisibilityConverterTests
{

    private readonly BooleanToVisibilityConverter _converter = new();

    [Theory]
    [InlineData(true, Visibility.Visible)]
    [InlineData(false, Visibility.Collapsed)]
    public void Convert_ShouldReturnExpectedVisibility_ForBooleanValue(bool input, Visibility expected)
    {
        // Act
        var result = _converter.Convert(input, typeof(Visibility), null, "en-US");

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, "true", Visibility.Collapsed)]
    [InlineData(false, "true", Visibility.Visible)]
    public void Convert_ShouldInvertVisibility_WhenParameterIsTrue(bool input, string parameter, Visibility expected)
    {
        // Act
        var result = _converter.Convert(input, typeof(Visibility), parameter, "en-US");

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_ShouldReturnCollapsed_WhenValueIsNotBool()
    {
        // Act
        var result = _converter.Convert("notBool", typeof(Visibility), null, "en-US");

        // Assert
        Assert.Equal(Visibility.Collapsed, result);
    }

    [Theory]
    [InlineData(Visibility.Visible, null, true)]
    [InlineData(Visibility.Collapsed, null, false)]
    [InlineData(Visibility.Visible, "true", false)]
    [InlineData(Visibility.Collapsed, "true", true)]
    public void ConvertBack_ShouldReturnExpectedBoolean(Visibility input, string parameter, bool expected)
    {
        // Act
        var result = _converter.ConvertBack(input, typeof(bool), parameter, "en-US");

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertBack_ShouldReturnFalse_WhenValueIsNotVisibility()
    {
        // Act
        var result = _converter.ConvertBack("invalid", typeof(bool), null, "en-US");

        // Assert
        Assert.False((bool)result);
    }
}
