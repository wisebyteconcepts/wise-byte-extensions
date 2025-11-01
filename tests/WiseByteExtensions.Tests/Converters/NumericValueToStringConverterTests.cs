using Microsoft.UI.Xaml.Data;

using System.Globalization;

using WiseByteExtensions.WinUI.Converters;

using Xunit;

namespace WiseByteExtensions.Tests.Converters;
public class NumericValueToStringConverterTests
{
    private readonly IValueConverter _converter = new NumericValueToStringConverter();

    [Theory]
    [InlineData(123.456, "0.00", "123.46")]
    [InlineData(0, "0.00", "0.00")]
    [InlineData(1234.5, "N0", "1,234")]
    [InlineData(1234.5, "N2", "1,234.50")]
    public void Convert_ValidNumericValues_FormatsCorrectly(double input, string format, string expected)
    {
        // Arrange
        var culture = CultureInfo.GetCultureInfo("en-US");

        // Act
        var result = _converter.Convert(input, typeof(string), format, culture.Name);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_UsesDefaultFormat_WhenParameterIsNull()
    {
        // Arrange
        double input = 12.345;

        // Act
        var result = _converter.Convert(input, typeof(string), null, "en-US");

        // Assert
        Assert.Equal("12.35", result);
    }

    [Theory]
    [InlineData(null, "0.00", "null")]
    [InlineData("not a number", "0.00", "NaN")]
    [InlineData(true, "0.00", "NaN")]
    public void Convert_NonNumericOrNullValues_ReturnsExpected(object input, string format, string expected)
    {
        // Act
        var result = _converter.Convert(input, typeof(string), format, "en-US");

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_FormatsCurrency_CorrectlyForCulture()
    {
        // Arrange
        double input = 1234.5;

        // Act
        var resultIn = _converter.Convert(input, typeof(string), "C2", "en-IN");
        var resultEn = _converter.Convert(input, typeof(string), "C2", "en-US");
        var resultDe = _converter.Convert(input, typeof(string), "C2", "de-DE");

        // Assert
        Assert.StartsWith("₹", resultIn.ToString());
        Assert.StartsWith("$", resultEn.ToString());
        Assert.EndsWith("€", resultDe.ToString());
    }

    [Theory]
    [InlineData("123.45", typeof(double), 123.45)]
    [InlineData("0", typeof(int), 0)]
    [InlineData("12.3", typeof(float), 12.3f)]
    [InlineData("123.456", typeof(decimal), 123.456)]
    [InlineData("$2,000.00", typeof(decimal), 2000)]
    public void ConvertBack_ValidNumericString_ReturnsExpectedTypeAndValue(string input, Type targetType, object expected)
    {
        // Act
        var result = _converter.ConvertBack(input, targetType, null, "en-US");

        // Assert
        Assert.IsType(targetType, result);
        Assert.Equal(Convert.ChangeType(expected, targetType), result);
    }

    [Theory]
    [InlineData("null", null)]
    [InlineData("NULL", null)]
    public void ConvertBack_NullString_ReturnsNull(string input, object expected)
    {
        // Act
        var result = _converter.ConvertBack(input, typeof(double), null, "en-US");

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData("NaN", double.NaN)]
    [InlineData("nan", double.NaN)]
    public void ConvertBack_NaNString_ReturnsDoubleNaN(string input, double expected)
    {
        // Act
        var result = _converter.ConvertBack(input, typeof(double), null, "en-US");

        // Assert
        Assert.IsType<double>(result);
        Assert.True(double.IsNaN((double)result));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ConvertBack_InvalidString_ReturnsZero(object input)
    {
        // Act
        var result = _converter.ConvertBack(input, typeof(double), null, "en-US");

        // Assert
        Assert.Equal(Convert.ToDouble(0), Convert.ToDouble(result));


    }

    [Fact]
    public void ConvertBack_UsesCulture_ForParsing()
    {
        // Arrange
        var input = "1.234,56"; // German decimal format

        // Act
        var result = _converter.ConvertBack(input, typeof(double), null, "de-DE");

        // Assert
        Assert.IsType<double>(result);
        Assert.Equal(1234.56, Math.Round((double)result, 2));
    }

    [Fact]
    public void ConvertBack_ConversionFailure_ReturnsZero()
    {
        // Arrange
        var input = "123.45";
        var targetType = typeof(DateTime); // not convertible

        // Act
        var result = _converter.ConvertBack(input, targetType, null, "en-US");

        // Assert
        Assert.Equal(0, result);
    }
}
