using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmEventTest
{
    private static event EventHandler<TestEventArgs>? TestEvent;

    private static void OnTestEvent(TestEventArgs e)
    {
        TestEvent?.Invoke(null, e);
    }

    private class TestEventArgs : EventArgs { }

    [TestCase]
    public static void TestRaises_WhenTestRaises()
    {
        Action action = static () => OnTestEvent(new());

        _ = action.ConfirmRaisesEvent(ref TestEvent);
    }

    [TestCase]
    public static void TestRaises_WhenTestDoesNotRaise()
    {
        Action action = static () =>
        {
            Action a = static () => { /* No event raise here */ };
            _ = a.ConfirmRaisesEvent(ref TestEvent);
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase]
    public static void TestDoesNotRaise_WhenTestDoesNotRaise()
    {
        Action action = static () => { /* No event raise here */ };

        _ = action.ConfirmDoesNotRaiseEvent(ref TestEvent);
    }

    [TestCase]
    public static void TestDoesNotRaise_WhenTestRaises()
    {
        Action action = static () =>
        {
            Action a = static () => OnTestEvent(new());
            _ = a.ConfirmDoesNotRaiseEvent(ref TestEvent);
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
}
