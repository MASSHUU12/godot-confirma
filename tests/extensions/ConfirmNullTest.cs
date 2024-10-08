using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmNullTest
{
    [TestCase(new object?[] { null })]
    public void ConfirmNull_WhenNull(object? actual)
    {
        _ = actual.ConfirmNull();
    }

    [TestCase("Lorem ipsum", "\"Lorem ipsum\"")]
    [TestCase(2, "2")]
    public void ConfirmNull_WhenNotNull(
        object? actual,
        string formatted
    )
    {
        Action action = () => actual.ConfirmNull();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            $"Assertion ConfirmNull failed: Expected null but got {formatted}."
        );
    }

    [TestCase("Lorem ipsum")]
    [TestCase(2)]
    public void ConfirmNotNull_WhenNotNull(object? actual)
    {
        _ = actual.ConfirmNotNull();
    }

    [TestCase(new object?[] { null })]
    public void ConfirmNotNull_WhenNull(object? actual)
    {
        Action action = () => actual.ConfirmNotNull();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotNull failed: Expected non-null value."
        );
    }
}
