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

        action.ConfirmThrows<NotImplementedException>();
    }

    [TestCase]
    public static void ConfirmThrows_WhenNotThrows()
    {
        Action action = () =>
        {
            Action a = () => {/* Not throws */};

            a.ConfirmThrows<NotImplementedException>();
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmThrows
    [TestCase]
    public static void ConfirmNotThrows_WhenNotThrows()
    {
        Action action = () => {/* Not throws */};

        action.ConfirmNotThrows<NotImplementedException>();
    }

    [TestCase]
    public static void ConfirmNotThrows_WhenThrows()
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException();

            a.ConfirmNotThrows<NotImplementedException>();
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmThrowsWMessage
    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public static void ConfirmThrowsWMessage_WhenThrows(string actual)
    {
        Action action = () => throw new NotImplementedException(actual);

        action.ConfirmThrowsWMessage<NotImplementedException>(actual);
    }

    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public static void ConfirmThrowsWMessage_WhenNotThrows(string actual)
    {
        Action action = () =>
        {
            Action a = () => {/* Not throws */};

            a.ConfirmThrowsWMessage<NotImplementedException>(actual);
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("", "Expected")]
    [TestCase("Actual", "")]
    [TestCase("Lorem ipsum", "dolor sit amet")]
    public static void ConfirmThrowsWMessage_WhenThrowsWWrongMessage(string actual, string expected)
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            a.ConfirmThrowsWMessage<NotImplementedException>(expected);
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmNotThrowsWMessage
    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public static void ConfirmNotThrowsWMessage_WhenNotThrows(string actual)
    {
        Action action = () => {/* Not throws */};

        action.ConfirmNotThrowsWMessage<NotImplementedException>(actual);
    }

    [TestCase("")]
    [TestCase("Lorem ipsum")]
    public static void ConfirmNotThrowsWMessage_WhenThrows(string actual)
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            a.ConfirmNotThrowsWMessage<NotImplementedException>(actual);
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("", "Expected")]
    [TestCase("Actual", "")]
    [TestCase("Lorem ipsum", "dolor sit amet")]
    public static void ConfirmNotThrowsWMessage_WhenThrowsWWrongMessage(string actual, string expected)
    {
        Action action = () =>
        {
            Action a = () => throw new NotImplementedException(actual);

            a.ConfirmNotThrowsWMessage<NotImplementedException>(expected);
        };

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion
}
