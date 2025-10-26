using WiseByteExtensions.WinUI.Converters;

using Xunit;

namespace WiseByteExtensions.Tests.Converters;
public class ThreeStateBooleanToSelectionConverterTests
{
    private readonly ThreeStateBooleanToSelectionConverter _converter = new();

    [Theory]
    // value, parameter, expected result
    [InlineData(true, "true", true)]
    [InlineData(true, "false", false)]
    [InlineData(true, null, false)]
    [InlineData(false, "true", false)]
    [InlineData(false, "false", true)]
    [InlineData(false, null, false)]
    [InlineData(null, "true", false)]
    [InlineData(null, "false", false)]
    [InlineData(null, null, true)]

    public void Convert(object? value, string? parameter, bool? expected)
    {
        var result = _converter.Convert(value, typeof(bool?), parameter, null);
        Assert.Equal(expected, result);
    }

    [Theory]
    // value (isChecked), parameter, expected result
    [InlineData(true, "true", true)]
    [InlineData(true, "false", false)]
    [InlineData(true, "null", null)]
    [InlineData(false, "true", false)]
    [InlineData(false, "false", true)]
    [InlineData(false, "null", null)]

    public void ConvertBack(bool? value, string parameter, bool? expected)
    {
        var result = _converter.ConvertBack(value, typeof(bool?), parameter, null);
        Assert.Equal(expected, result);
    }
}
