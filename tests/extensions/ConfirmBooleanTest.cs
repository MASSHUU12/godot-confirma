using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmBooleanTest
{
    [TestCase]
    public void ConfirmTrue_WhenTrue()
    {
        _ = true.ConfirmTrue();
    }

    [TestCase]
    public void ConfirmTrue_WhenFalse()
    {
        Action action = static () => false.ConfirmTrue();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmTrue failed: Expected True but was False."
        );
    }

    [TestCase]
    public void ConfirmFalse_WhenFalse()
    {
        _ = false.ConfirmFalse();
    }

    [TestCase]
    public void ConfirmFalse_WhenTrue()
    {
        Action action = static () => true.ConfirmFalse();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmFalse failed: Expected False but was True."
        );
    }
}
