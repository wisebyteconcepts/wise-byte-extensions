using WiseByteExtensions.WinUI.Converters;

using Xunit;

namespace WiseByteExtensions.Tests.Converters;
public class BooleanToSelectionConverterTests
{
    private readonly BooleanToSelectionConverter _converter = new();

    [Theory]
    // value, parameter, expected result
    [InlineData(true, "true", true)]
    [InlineData(true, "false", false)]
    [InlineData(true, null, false)]
    [InlineData(false, "true", false)]
    [InlineData(false, "false", true)]
    [InlineData(false, null, false)]
    public void Convert(object value, string? parameter, bool expected)
    {
        var result = _converter.Convert(value, typeof(bool), parameter, null);
        Assert.Equal(expected, result);
    }

    [Theory]
    // value (isChecked), parameter, expected result
    [InlineData(true, "true", true)]
    [InlineData(true, "false", false)]
    [InlineData(true, null, false)]
    [InlineData(false, "true", false)]
    [InlineData(false, "false", true)]
    [InlineData(false, null, false)]
    public void ConvertBack(bool value, string? parameter, bool? expected)
    {
        var result = _converter.ConvertBack(value, typeof(bool), parameter, null);
        Assert.Equal(expected, result);
    }
}
