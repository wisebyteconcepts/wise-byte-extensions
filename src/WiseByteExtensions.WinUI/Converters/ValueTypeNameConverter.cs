using Microsoft.UI.Xaml.Data;

using System;
using System.Collections.Generic;

namespace WiseByteExtensions.WinUI.Converters;

/// <summary>
/// A value converter for WinUI that returns the type name of a given value, 
/// using C# built-in type aliases (e.g., <c>int</c>, <c>double</c>, <c>string</c>) when applicable. 
/// This is useful for displaying the data type of bound values in the UI, 
/// such as in debugging or inspection tools.
/// </summary>
/// <remarks>
/// The converter inspects the runtime type of the provided <paramref name="value"/> and 
/// returns a user-friendly string representing its type name.
/// 
/// <list type="bullet">
/// <item><description>For common .NET primitive types, the converter returns their C# alias name (e.g., <c>int</c> instead of <c>Int32</c>).</description></item>
/// <item><description>For custom or complex types, it returns the type’s <see cref="Type.Name"/> property.</description></item>
/// <item><description>If the input <paramref name="value"/> is <c>null</c>, the converter returns the string "null".</description></item>
/// </list>
/// 
/// Example usage with a TextBlock:
/// <code>
/// &lt;TextBlock Text="{Binding SomeValue, 
///                            Converter={StaticResource ValueTypeNameConverter}}" /&gt;
/// </code>
/// 
/// In this example:
/// - If <c>SomeValue</c> is an <see cref="System.Int32"/>, the TextBlock will display "int".
/// - If <c>SomeValue</c> is a <see cref="System.String"/>, it will display "string".
/// - If <c>SomeValue</c> is a custom class like <c>Person</c>, it will display "Person".
/// </remarks>
/// <param name="value">The input value whose type name is to be determined.</param>
/// <param name="targetType">The target binding type (ignored).</param>
/// <param name="parameter">Optional parameter (not used).</param>
/// <param name="language">The culture/language (ignored).</param>
/// <returns>
/// A string representing the C# alias or type name of the input value, 
/// or "null" if the input is <c>null</c>.
/// </returns>
public partial class ValueTypeNameConverter : IValueConverter
{
    private static readonly Dictionary<Type, string> AliasMap = new()
        {
            { typeof(int), "int" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(decimal), "decimal" },
            { typeof(long), "long" },
            { typeof(short), "short" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },
            { typeof(ushort), "ushort" },
            { typeof(bool), "bool" },
            { typeof(string), "string" }
        };
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return "null";

        var type = value.GetType();
        return AliasMap.TryGetValue(type, out var alias)
            ? alias
            : type.Name;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
