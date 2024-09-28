using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmSignalTest
{
    private static Button? _button;

    [SetUp]
    public static void SetUp()
    {
        _button = new();
    }

    [TearDown]
    public static void TearDown()
    {
        _button?.Free();
    }

    #region ConfirmSignalExists
    [TestCase("pressed")]
    [TestCase("toggled")]
    [TestCase("button_down")]
    public static void ConfirmSignalExists_WhenSignalExists(string signalName)
    {
        _ = (_button?.ConfirmSignalExists((StringName)signalName));
    }

    [TestCase("signal_that_does_not_exist")]
    [TestCase("signal_that_is_not_here")]
    [TestCase("signal_that_is_nowhere_to_be_found")]
    public static void ConfirmSignalExists_WhenSignalDoesNotExist(
        string signalName
    )
    {
        Action action = () => _button?.ConfirmSignalExists((StringName)signalName);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmSignalExists failed: "
            + $"Expected signal {signalName} to exist on object of type Button."
        );
    }
    #endregion ConfirmSignalExists

    #region ConfirmSignalDoesNotExist
    [TestCase("signal_that_does_not_exist")]
    [TestCase("signal_that_is_not_here")]
    [TestCase("signal_that_is_nowhere_to_be_found")]
    public static void ConfirmSignalDoesNotExist_WhenSignalDoesNotExist(
        string signalName
    )
    {
        _ = (_button?.ConfirmSignalDoesNotExist((StringName)signalName));
    }

    [TestCase("pressed")]
    [TestCase("toggled")]
    [TestCase("button_down")]
    public static void ConfirmSignalDoesNotExist_WhenSignalExists(
        string signalName
    )
    {
        Action action = () => _button?.ConfirmSignalDoesNotExist(
            (StringName)signalName
        );

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmSignalDoesNotExist failed: "
            + $"Expected signal {signalName} to not exist on object of type Button."
        );
    }
    #endregion ConfirmSignalDoesNotExist
}
