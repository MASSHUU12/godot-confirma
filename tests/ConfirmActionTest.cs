using System;
using System.Threading;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmActionTest
{
    [TestCase]
    public static void ConfirmCompletesWithin_WhenCompletesWithin()
    {
        Action action = () => Thread.Sleep(5);

        action.ConfirmCompletesWithin(TimeSpan.FromMilliseconds(50));
    }

    [TestCase]
    public static void ConfirmCompletesWithin_WhenDoesNotCompletesWithin()
    {
        Action action = () =>
        {
            Action a = () => Thread.Sleep(10);

            a.ConfirmCompletesWithin(TimeSpan.FromMilliseconds(5));
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase]
    public static void ConfirmDoesNotCompleteWithin_WhenDoesNotCompleteWithin()
    {
        Action action = () => Thread.Sleep(50);

        action.ConfirmDoesNotCompleteWithin(TimeSpan.FromMilliseconds(5));
    }

    [TestCase]
    public static void ConfirmDoesNotCompleteWithin_WhenCompletesWithin()
    {
        Action action = () =>
        {
            Action a = () => Thread.Sleep(5);

            a.ConfirmDoesNotCompleteWithin(TimeSpan.FromMilliseconds(50));
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }
}
