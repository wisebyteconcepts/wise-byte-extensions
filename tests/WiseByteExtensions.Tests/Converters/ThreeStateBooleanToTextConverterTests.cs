using WiseByteExtensions.WinUI.Converters;

using Xunit;

namespace WiseByteExtensions.Tests.Converters;
public class ThreeStateBooleanToTextConverterTests
{
    private readonly ThreeStateBooleanToTextConverter _converter = new();

    [Theory]
    // value, parameter, expected result
    [InlineData(true, "TrueText", "null")]
    [InlineData(true, "TrueText|FalseText", "null")]
    [InlineData(true, "TrueText|FalseText|NullText", "TrueText")]
    [InlineData(true, "TrueText|FalseText|NullText|AnotherText", "TrueText")]
    [InlineData(true, "TrueText;FalseText", "null")]
    [InlineData(true, null, "null")]
    [InlineData(true, "", "null")]
    [InlineData(false, "FalseText", "null")]
    [InlineData(false, "TrueText|FalseText", "null")]
    [InlineData(false, "TrueText|FalseText|NullText", "FalseText")]
    [InlineData(false, "TrueText|FalseText|NullText|AnotherText", "FalseText")]
    [InlineData(false, "TrueText;FalseText", "null")]
    [InlineData(false, null, "null")]
    [InlineData(false, "", "null")]
    [InlineData(null, "FalseText", "null")]
    [InlineData(null, "TrueText|FalseText", "null")]
    [InlineData(null, "TrueText|FalseText|NullText", "NullText")]
    [InlineData(null, "TrueText|FalseText|NullText|AnotherText", "NullText")]
    [InlineData(null, "TrueText;FalseText", "null")]
    [InlineData(null, null, "null")]
    [InlineData(null, "", "null")]

    public void Convert(object? value, string? parameter, string expected)
    {
        var result = _converter.Convert(value, typeof(bool?), parameter, string.Empty);
        Assert.Equal(expected, result);
    }


    [Theory]
    // value, parameter, expected result
    [InlineData("TrueText", "TrueText|FalseText|NullText", true)]
    [InlineData("FalseText", "TrueText|FalseText|NullText", false)]
    [InlineData("NullText", "TrueText|FalseText|NullText", null)]
    [InlineData("NullText", "", null)]
    [InlineData("NullText", "null", null)]
    [InlineData("NullText", null, null)]
    public void ConvertBack(object? value, string? parameter, object? expected)
    {
        var result = _converter.ConvertBack(value, typeof(bool?), parameter, string.Empty);
        Assert.Equal(expected, result);
    }
}
