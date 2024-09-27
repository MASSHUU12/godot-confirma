using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class VariantFormatterTest
{
    private static readonly VariantFormatter _formatter = new();

    [TestCase]
    public static void Format_String_CorrectlyFormats()
    {
        _ = _formatter
            .Format(Variant.From("Hello, World!"))
            .ConfirmEqual("\"Hello, World!\"");
    }

    [Ignore(
        Reason =
            "All chars are converted to Int64, so there is no easy way "
            + "to distinguish them from regular numbers",
        HideFromResults = true
    )]
    [TestCase]
    public static void Format_Char_CorrectlyFormats()
    {
        _ = _formatter
            .Format(Variant.From('c')) // 99 (decimal) in ASCII
            .ConfirmEqual("'c'");
    }

    [TestCase]
    public static void Format_Number_CorrectlyFormats()
    {
        _ = _formatter
            .Format(Variant.From(12345))
            .ConfirmEqual("12,345");
    }

    [TestCase]
    public static void Format_Array_CorrectlyFormats()
    {
        _ = _formatter
            .Format(new int[] { 1, 2, 3 })
            .ConfirmEqual("[1, 2, 3]");
    }
}
