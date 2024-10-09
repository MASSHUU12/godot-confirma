using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmUuidExtensionsTest
{
    private static readonly Random rg = new();

    #region ConfirmValidUuid4
    [Repeat(3)]
    [TestCase]
    public void ConfirmValidUuid4_WhenValid()
    {
        _ = rg.NextUuid4().ToString().ConfirmValidUuid4();
    }

    [TestCase("aaa")]
    [TestCase("aaa-aaa-aaa-aaa")]
    [TestCase("Lorem ipsum dolor sit amet")]
    public void ConfirmValidUuid4_WhenInvalid(string actual)
    {
        Action action = () => actual.ConfirmValidUuid4();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmValidUuid4 failed: "
            + $"Expected a valid UUID, but got {actual}."
        );
    }
    #endregion ConfirmValidUuid4

    #region ConfirmInvalidUuid4
    [TestCase("aaa")]
    [TestCase("aaa-aaa-aaa-aaa")]
    [TestCase("Lorem ipsum dolor sit amet")]
    public void ConfirmInvalidUuid4_WhenInvalid(string actual)
    {
        _ = actual.ConfirmInvalidUuid4();
    }

    [Repeat(3)]
    [TestCase]
    public void ConfirmInvalidUuid4_WhenValid()
    {
        string actual = rg.NextUuid4().ToString();
        Action action = () => actual.ConfirmInvalidUuid4();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmInvalidUuid4 failed: "
            + $"Expected invalid UUID, but got {actual}."
        );
    }
    #endregion ConfirmInvalidUuid4
}
