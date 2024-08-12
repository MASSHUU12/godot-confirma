using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmNullTest
{
    [TestCase(new object?[] { null })]
    public static void ConfirmNull_WhenNull(object? actual)
    {
        _ = actual.ConfirmNull();
    }

    [TestCase("Lorem ipsum")]
    [TestCase(2)]
    public static void ConfirmNull_WhenNotNull(object? actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmNull());
    }

    [TestCase("Lorem ipsum")]
    [TestCase(2)]
    public static void ConfirmNotNull_WhenNotNull(object? actual)
    {
        _ = actual.ConfirmNotNull();
    }

    [TestCase(new object?[] { null })]
    public static void ConfirmNotNull_WhenNull(object? actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmNotNull());
    }
}
