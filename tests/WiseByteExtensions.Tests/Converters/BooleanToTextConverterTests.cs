using WiseByteExtensions.WinUI.Converters;

using Xunit;

namespace WiseByteExtensions.Tests.Converters;
public class BooleanToTextConverterTests
{
    private readonly BooleanToTextConverter _converter = new();

    [Theory]
    // value, parameter, expected result
    [InlineData(true, "TrueText", "null")]
    [InlineData(true, "TrueText|FalseText", "TrueText")]
    [InlineData(true, "TrueText;FalseText", "null")]
    [InlineData(true, "TrueText|FalseText|AnotherText", "TrueText")]
    [InlineData(true, null, "null")]
    [InlineData(false, "FalseText", "null")]
    [InlineData(false, "TrueText|FalseText", "FalseText")]
    [InlineData(false, "TrueText;FalseText", "null")]
    [InlineData(false, "TrueText|FalseText|AnotherText", "FalseText")]
    [InlineData(false, null, "null")]
    public void Convert(object value, string? parameter, string expected)
    {
        var result = _converter.Convert(value, typeof(bool), parameter, string.Empty);
        Assert.Equal(expected, result);
    }



    [Theory]
    // convertedvalue, parameter, expected viewmodel value
    [InlineData("TrueText", "TrueText|FalseText", true)]
    [InlineData("FalseText", "TrueText|FalseText", false)]
    [InlineData("FalseText", "TrueText|FalseText|AnotherText", false)]
    [InlineData("TrueText", "TrueText;FalseText", false)]
    [InlineData("TrueText", "TrueText|FalseText|AnoterText", true)]
    [InlineData("TrueText", "", false)]
    [InlineData("TrueText", null, false)]

    public void ConvertBack(object value, object? parameter, bool expected)
    {
        var result = _converter.ConvertBack(value, typeof(bool), parameter, string.Empty);
        Assert.Equal(expected, result);
    }
}
