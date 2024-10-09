using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmExceptionTest
{
    #region ConfirmThrows
    [TestCase]
    public void ConfirmThrows_WhenThrows()
    {
        Action action = static () => throw new NotImplementedException();

        _ = action.ConfirmThrows<NotImplementedException>();
    }

    [TestCase]
    public void ConfirmThrows_WhenNotThrows()
    {
        Action action = static () =>
        {
            Action a = static () => {/* Not throws */};

            _ = a.ConfirmThrows<NotImplementedException>();
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmThrows failed: "
            + "Expected NotImplementedException to be thrown, "
            + "but no exception was thrown."
        );
    }
    #endregion ConfirmThrows

    #region ConfirmThrows
    [TestCase]
    public void ConfirmNotThrows_WhenNotThrows()
    {
        Action action = static () => {/* Not throws */};

        _ = action.ConfirmNotThrows<NotImplementedException>();
    }

    [TestCase]
    public void ConfirmNotThrows_WhenThrows()
    {
        Action action = static () =>
        {
            Action a = static () => throw new NotImplementedException();

            _ = a.ConfirmNotThrows<NotImplementedException>();
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotThrows failed: "
            + "Expected NotImplementedException not to be thrown."
        );
    }
    #endregion ConfirmThrows

    #region ConfirmThrowsWMessage
    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public void ConfirmThrowsWMessage_WhenThrows(string actual)
    {
        Action action = () => throw new NotImplementedException(actual);

        _ = action.ConfirmThrowsWMessage<NotImplementedException>(actual);
    }

    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public void ConfirmThrowsWMessage_WhenNotThrows(string actual)
    {
        Action action = () =>
        {
            Action a = () => {/* Not throws */};

            _ = a.ConfirmThrowsWMessage<NotImplementedException>(actual);
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmThrowsWMessage failed: "
            + "Expected NotImplementedException to be thrown, "
            + "but no exception was thrown."
        );
    }

    [TestCase("", "Expected")]
    [TestCase("Actual", "")]
    [TestCase("Lorem ipsum", "dolor sit amet")]
    public void ConfirmThrowsWMessage_WhenThrowsWWrongMessage(
        string actual,
        string expected
    )
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            _ = a.ConfirmThrowsWMessage<NotImplementedException>(expected);
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmThrowsWMessage failed: "
            + "Expected NotImplementedException to be thrown with message "
            + $"\"{expected}\" but got \"{actual}\" instead."
        );
    }
    #endregion ConfirmThrowsWMessage

    #region ConfirmNotThrowsWMessage
    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public void ConfirmNotThrowsWMessage_WhenNotThrows(string actual)
    {
        Action action = static () => {/* Not throws */};

        _ = action.ConfirmNotThrowsWMessage<NotImplementedException>(actual);
    }

    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public void ConfirmNotThrowsWMessage_WhenThrows(string actual)
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            _ = a.ConfirmNotThrowsWMessage<NotImplementedException>(actual);
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotThrowsWMessage failed: "
            + "Expected NotImplementedException not to be thrown with message "
            + $"\"{actual}\"."
        );
    }

    [TestCase("", "Expected")]
    [TestCase("Actual", "")]
    [TestCase("Lorem ipsum", "dolor sit amet")]
    public void ConfirmNotThrowsWMessage_WhenThrowsWWrongMessage(
        string actual,
        string expected
    )
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            _ = a.ConfirmNotThrowsWMessage<NotImplementedException>(expected);
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotThrowsWMessage failed: "
            + "Expected NotImplementedException not to be thrown with message "
            + $"\"{expected}\"."
        );
    }
    #endregion ConfirmNotThrowsWMessage
}
