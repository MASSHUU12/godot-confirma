using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class NumericFormatterTest
{
    private static NumericFormatter? _formatter;

    [SetUp]
    public static void SetUp()
    {
        _formatter = new();
    }

    [TestCase]
    public static void Format_NullValue_ReturnsNullString()
    {
        object? value = null;
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("null");
    }

    [TestCase]
    public static void Format_NonNumericValue_ReturnsDefaultFormatterResult()
    {
        object value = "Hello, World!";
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("Hello, World!");
    }

    [TestCase(12345, "12,345")]
    [TestCase(12345u, "12,345")]
    [TestCase((byte)123, "123")]
    [TestCase(123.456f, "123.45600")]
    public static void Format_ReturnsCorrectlyFormattedString(
        object actual,
        string expected
    )
    {
        _ = _formatter!.Format(actual).ConfirmEqual(expected);
    }

    [TestCase(12345, "12,345", 2)]
    [TestCase(123.456f, "123.5", 1)]
    [TestCase(50.12345678d, "50.12346", 5)]
    public static void Format_Precision_ReturnsCorrectlyFormattedString(
        object actual,
        string expected,
        int precision
    )
    {
        _ = new NumericFormatter((ushort)precision)
            .Format(actual)
            .ConfirmEqual(expected);
    }

    [TestCase]
    public static void Format_NullableObject_ReturnsCorrectlyFormattedString()
    {
        object? value = 5.0001f;
        _ = new NumericFormatter(2).Format(value).ConfirmEqual("5.00");
    }

    [TestCase]
    public static void Format_NullableFloat_ReturnsCorrectlyFormattedString()
    {
        float? value = 5.0001f;
        _ = new NumericFormatter(2).Format(value).ConfirmEqual("5.00");
    }
}
