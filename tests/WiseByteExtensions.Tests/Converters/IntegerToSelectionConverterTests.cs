using WiseByteExtensions.WinUI.Converters;

using Xunit;

namespace WiseByteExtensions.Tests.Converters;
public class IntegerToSelectionConverterTests
{
    private readonly IntegerToSelectionConverter _converter = new();

    [Theory]
    // value, parameter, expected result
    [InlineData(0, "0", true)]
    [InlineData(0, "1", false)]
    [InlineData(1, "0", false)]
    [InlineData(1, "1", true)]
    [InlineData(1, "2", false)]
    [InlineData(2, "2", true)]
    [InlineData(2, "1", false)]
    public void Convert(object value, string parameter, bool expected)
    {
        var result = _converter.Convert(value, typeof(bool), parameter, string.Empty);
        Assert.Equal(expected, result);
    }

    [Theory]
    // value, parameter, expected result
    [InlineData(true, "0", 0)]
    [InlineData(true, "1", 1)]
    public void ConvertBack(object value, string parameter, object expected)
    {
        var result = _converter.ConvertBack(value, typeof(bool), parameter, string.Empty);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(false, "0")]
    [InlineData(false, "1")]
    public void ConvertBack_ShouldThrowNotSupportedException_WhenValueIsFalse(object value, string parameter)
    {
        // Act + Assert
        Assert.Throws<NotSupportedException>(() =>
            _converter.ConvertBack(value, typeof(int), parameter, string.Empty));
    }

}
