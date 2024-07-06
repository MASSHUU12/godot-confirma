using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmBooleanTest
{
    [TestCase]
    public static void ConfirmTrue_WhenTrue()
    {
        true.ConfirmTrue();
    }

    [TestCase]
    public static void ConfirmTrue_WhenFalse()
    {
        Action action = () => false.ConfirmTrue();

        action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase]
    public static void ConfirmFalse_WhenFalse()
    {
        false.ConfirmFalse();
    }

    [TestCase]
    public static void ConfirmFalse_WhenTrue()
    {
        Action action = () => true.ConfirmFalse();

        action.ConfirmThrows<ConfirmAssertException>();
    }
}
