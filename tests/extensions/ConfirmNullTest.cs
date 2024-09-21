using System;
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
        Action action = () => actual.ConfirmNull();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            $"Assertion ConfirmNull failed: Expected null but got {actual}."
        );
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
        Action action = () => actual.ConfirmNotNull();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotNull failed: Expected non-null value."
        );
    }
}
