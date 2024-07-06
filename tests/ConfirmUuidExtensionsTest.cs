using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmUuidExtensions
{
    private static readonly Random rg = new();

    #region ConfirmValidUuid4
    [TestCase]
    public static void ConfirmValidUuid4_WhenValid()
    {
        rg.NextUuid4().ToString().ConfirmValidUuid4();
        rg.NextUuid4().ToString().ConfirmValidUuid4();
        rg.NextUuid4().ToString().ConfirmValidUuid4();
    }

    [TestCase("aaa")]
    [TestCase("aaa-aaa-aaa-aaa")]
    [TestCase("Lorem ipsum dolor sit amet")]
    public static void ConfirmValidUuid4_WhenInvalid(string actual)
    {
        Action action = () => actual.ConfirmValidUuid4();

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmInvalidUuid4
    [TestCase("aaa")]
    [TestCase("aaa-aaa-aaa-aaa")]
    [TestCase("Lorem ipsum dolor sit amet")]
    public static void ConfirmInvalidUuid4_WhenInvalid(string actual)
    {
        actual.ConfirmInvalidUuid4();
    }

    [TestCase]
    public static void ConfirmInvalidUuid4_WhenValid()
    {
        Action action = () => rg.NextUuid4().ToString().ConfirmInvalidUuid4();

        action.ConfirmThrows<ConfirmAssertException>();
        action.ConfirmThrows<ConfirmAssertException>();
        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion
}
