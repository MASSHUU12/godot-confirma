using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Wrappers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class WrapperBaseTest
{
    [TestCase("", null)]
    [TestCase(null, null)]
    [TestCase("Lorem", "Lorem")]
    public static void ParseMessage_ReturnsCorrectValue(
        string? actual,
        string? expected
    )
    {
        _ = WrapperBase.ParseMessage(actual).ConfirmEqual(expected);
    }
}