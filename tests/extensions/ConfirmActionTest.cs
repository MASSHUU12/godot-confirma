using System;
using System.Threading;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmActionTest
{
    [TestCase]
    public static void ConfirmCompletesWithin_WhenCompletesWithin()
    {
        Action action = static () => Thread.Sleep(5);

        _ = action.ConfirmCompletesWithin(TimeSpan.FromMilliseconds(50));
    }

    [TestCase]
    public static void ConfirmCompletesWithin_WhenDoesNotCompletesWithin()
    {
        Action action = static () =>
        {
            Action a = static () => Thread.Sleep(10);

            _ = a.ConfirmCompletesWithin(TimeSpan.FromMilliseconds(5));
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmCompletesWithin failed: "
            + "Expected action to complete within 5.00 ms, but it timed out."
        );
    }

    [TestCase]
    public static void ConfirmDoesNotCompleteWithin_WhenDoesNotCompleteWithin()
    {
        Action action = static () => Thread.Sleep(50);

        _ = action.ConfirmDoesNotCompleteWithin(TimeSpan.FromMilliseconds(5));
    }

    [TestCase]
    public static void ConfirmDoesNotCompleteWithin_WhenCompletesWithin()
    {
        Action action = static () =>
        {
            Action a = static () => Thread.Sleep(5);

            _ = a.ConfirmDoesNotCompleteWithin(TimeSpan.FromMilliseconds(50));
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmDoesNotCompleteWithin failed: "
            + "Expected action to not complete within 50.00 ms, but it did."
        );
    }
}
