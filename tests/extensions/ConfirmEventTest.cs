using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmEventTest
{
    private static event EventHandler<TestEventArgs>? TestEvent;
    private static event EventHandler<TestEventArgs>? NullEvent = null;
    private static event EventHandler<TestEventArgs>? FieldInfoEvent = (e, o) => { }; //second "unknown"

    private static void OnEvent(TestEventArgs e, EventHandler<TestEventArgs>? handler)
    {
        handler?.Invoke(null, e);
    }

    private class TestEventArgs : EventArgs { }

    [TestCase]
    public void ConfirmRaisesEvent_WhenTestRaises()
    {
        Action action = static () => OnEvent(new(), TestEvent);

        _ = action.ConfirmRaisesEvent(ref TestEvent);
    }

    [TestCase]
    public void ConfirmRaisesEvent_WhenTestDoesNotRaise()
    {
        Action action = static () =>
        {
            Action a = static () => { /* No event raise here */ };
            _ = a.ConfirmRaisesEvent(ref TestEvent);
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase]
    public void ConfirmRaisesEvent_WhenEventHandlerIsNull()
    {
        Action action = static () =>
        {
            Action a = static () => { /* No event raise here */ };
            _ = a.ConfirmRaisesEvent(ref NullEvent);
        };

        action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmRaisesEvent failed: " +
            "Expected event null to be raised."
        );
    }

    [TestCase]
    public void ConfirmRaisesEvent_WhenEventHandlerIsUnknown()
    {
        Action action = static () =>
        {
            Action a = static () => { /* No event raise here */ };
            _ = a.ConfirmRaisesEvent(ref FieldInfoEvent);
        };

        action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmRaisesEvent failed: " +
            "Expected event unknown to be raised."
        );
    }

    [TestCase]
    public void ConfirmDoesNotRaiseEvent_WhenTestDoesNotRaise()
    {
        Action action = static () => { /* No event raise here */ };

        _ = action.ConfirmDoesNotRaiseEvent(ref TestEvent);
    }

    [TestCase]
    public void ConfirmDoesNotRaiseEvent_WhenTestRaises()
    {
        Action action = static () =>
        {
            Action a = static () => OnEvent(new(), TestEvent);
            _ = a.ConfirmDoesNotRaiseEvent(ref TestEvent);
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase]
    public void ConfirmDoesNotRaiseEvent_WhenEventHandlerIsNull()
    {
        Action action = static () =>
        {
            Action a = static () => OnEvent(new(), NullEvent);

            _ = a.ConfirmDoesNotRaiseEvent(ref NullEvent);
        };

        action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmRaisesEvent failed: " +
            "Expected event null not to be raised."
        );
    }

    [TestCase]
    public void ConfirmDoesNotRaisesEvent_WhenEventHandlerIsUnknown()
    {
        Action action = static () =>
        {
            Action a = static () => OnEvent(new(), FieldInfoEvent);

            _ = a.ConfirmDoesNotRaiseEvent(ref FieldInfoEvent);
        };

        action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmRaisesEvent failed: " +
            "Expected event unknown not to be raised."
        );
    }
}
