using Microsoft.UI.Xaml;

using WiseByteExtensions.WinUI.Converters;

using Xunit;

namespace WiseByteExtensions.Tests.Converters;
public class IntergerToVisibilityConverterTests
{
    private readonly IntegerToVisibilityConverter _converter = new();

    [Theory]
    // value, parameter, expected result
    [InlineData(0, "0", Visibility.Visible)]
    [InlineData(0, "1", Visibility.Collapsed)]
    [InlineData(1, "0|1", Visibility.Visible)]
    [InlineData(1, "1", Visibility.Visible)]
    [InlineData(1, "2", Visibility.Collapsed)]
    [InlineData(2, "2", Visibility.Visible)]
    [InlineData(2, "1", Visibility.Collapsed)]
    public void Convert(object value, string parameter, Visibility expected)
    {
        var result = _converter.Convert(value, typeof(bool), parameter, string.Empty);
        Assert.Equal(expected, result);
    }
}
