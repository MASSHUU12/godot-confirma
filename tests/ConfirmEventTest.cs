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
        Action action = () => OnTestEvent(new());

        action.ConfirmRaisesEvent(ref TestEvent);
    }

    [TestCase]
    public static void TestRaises_WhenTestDoesNotRaise()
    {
        Action action = () =>
        {
            Action a = () => { /* No event raise here */ };
            a.ConfirmRaisesEvent(ref TestEvent);
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase]
    public static void TestDoesNotRaise_WhenTestDoesNotRaise()
    {
        Action action = () => { /* No event raise here */ };

        action.ConfirmDoesNotRaiseEvent(ref TestEvent);
    }

    [TestCase]
    public static void TestDoesNotRaise_WhenTestRaises()
    {
        Action action = () =>
        {
            Action a = () => OnTestEvent(new());
            a.ConfirmDoesNotRaiseEvent(ref TestEvent);
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }
}
