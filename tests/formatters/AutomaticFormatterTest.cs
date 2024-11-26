using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class AutomaticFormatterTest
{
    private static readonly AutomaticFormatter _formatter = new();

    [TestCase('c', "'c'")]
    [TestCase(true, "True")]
    [TestCase(12345, "12,345")]
    [TestCase(12345u, "12,345")]
    [TestCase((byte)123, "123")]
    [TestCase(123.456f, "123.45600")]
    [TestCase(new int[] { 1, 2, 3 }, "Int32[1, 2, 3]")]
    [TestCase("Hello, World!", "\"Hello, World!\"")]
    public void Format_CorrectlyFormatsSupportedTypes(
        object actual,
        string expected
    )
    {
        _ = _formatter.Format(actual).ConfirmEqual(expected);
    }
}
