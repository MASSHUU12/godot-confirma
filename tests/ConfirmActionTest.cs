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
        Action action = () => Thread.Sleep(5);

        _ = action.ConfirmCompletesWithin(TimeSpan.FromMilliseconds(50));
    }

    [TestCase]
    public static void ConfirmCompletesWithin_WhenDoesNotCompletesWithin()
    {
        Action action = () =>
        {
            Action a = () => Thread.Sleep(10);

            _ = a.ConfirmCompletesWithin(TimeSpan.FromMilliseconds(5));
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase]
    public static void ConfirmDoesNotCompleteWithin_WhenDoesNotCompleteWithin()
    {
        Action action = () => Thread.Sleep(50);

        _ = action.ConfirmDoesNotCompleteWithin(TimeSpan.FromMilliseconds(5));
    }

    [TestCase]
    public static void ConfirmDoesNotCompleteWithin_WhenCompletesWithin()
    {
        Action action = () =>
        {
            Action a = () => Thread.Sleep(5);

            _ = a.ConfirmDoesNotCompleteWithin(TimeSpan.FromMilliseconds(50));
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
}
