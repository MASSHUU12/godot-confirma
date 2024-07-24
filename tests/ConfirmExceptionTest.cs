using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmExceptionTest
{
    #region ConfirmThrows
    [TestCase]
    public static void ConfirmThrows_WhenThrows()
    {
        Action action = () => throw new NotImplementedException();

        _ = action.ConfirmThrows<NotImplementedException>();
    }

    [TestCase]
    public static void ConfirmThrows_WhenNotThrows()
    {
        Action action = () =>
        {
            Action a = () => {/* Not throws */};

            _ = a.ConfirmThrows<NotImplementedException>();
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmThrows

    #region ConfirmThrows
    [TestCase]
    public static void ConfirmNotThrows_WhenNotThrows()
    {
        Action action = () => {/* Not throws */};

        _ = action.ConfirmNotThrows<NotImplementedException>();
    }

    [TestCase]
    public static void ConfirmNotThrows_WhenThrows()
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException();

            _ = a.ConfirmNotThrows<NotImplementedException>();
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmThrows

    #region ConfirmThrowsWMessage
    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public static void ConfirmThrowsWMessage_WhenThrows(string actual)
    {
        Action action = () => throw new NotImplementedException(actual);

        _ = action.ConfirmThrowsWMessage<NotImplementedException>(actual);
    }

    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public static void ConfirmThrowsWMessage_WhenNotThrows(string actual)
    {
        Action action = () =>
        {
            Action a = () => {/* Not throws */};

            _ = a.ConfirmThrowsWMessage<NotImplementedException>(actual);
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("", "Expected")]
    [TestCase("Actual", "")]
    [TestCase("Lorem ipsum", "dolor sit amet")]
    public static void ConfirmThrowsWMessage_WhenThrowsWWrongMessage(string actual, string expected)
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            _ = a.ConfirmThrowsWMessage<NotImplementedException>(expected);
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmThrowsWMessage

    #region ConfirmNotThrowsWMessage
    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public static void ConfirmNotThrowsWMessage_WhenNotThrows(string actual)
    {
        Action action = () => {/* Not throws */};

        _ = action.ConfirmNotThrowsWMessage<NotImplementedException>(actual);
    }

    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public static void ConfirmNotThrowsWMessage_WhenThrows(string actual)
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            _ = a.ConfirmNotThrowsWMessage<NotImplementedException>(actual);
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("", "Expected")]
    [TestCase("Actual", "")]
    [TestCase("Lorem ipsum", "dolor sit amet")]
    public static void ConfirmNotThrowsWMessage_WhenThrowsWWrongMessage(string actual, string expected)
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            _ = a.ConfirmNotThrowsWMessage<NotImplementedException>(expected);
        };

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotThrowsWMessage
}
