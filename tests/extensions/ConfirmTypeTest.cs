using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmTypeTest
{
    #region ConfirmType
    [TestCase("Lorem ipsum", typeof(string))]
    [TestCase(42, typeof(int))]
    [TestCase(3.14, typeof(double))]
    [TestCase(true, typeof(bool))]
    public void ConfirmType_WhenOfType(object? actual, Type expected)
    {
        _ = actual.ConfirmType(expected);
    }

    [TestCase("Lorem ipsum", typeof(int))]
    [TestCase(42, typeof(double))]
    [TestCase(3.14, typeof(bool))]
    public void ConfirmType_WhenNotOfType(object? actual, Type expected)
    {
        Action action = () => actual.ConfirmType(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmType failed: "
            + $"Expected object of type {expected.Name}, "
            + $"but got {actual?.GetType().Name}."
        );
    }
    #endregion ConfirmType

    #region ConfirmNotType
    [TestCase("Lorem ipsum", typeof(int))]
    [TestCase(42, typeof(double))]
    [TestCase(3.14, typeof(bool))]
    public void ConfirmNotType_WhenNotOfType(
        object? actual,
        Type expected
    )
    {
        _ = actual.ConfirmNotType(expected);
    }

    [TestCase("Lorem ipsum", typeof(string))]
    [TestCase(42, typeof(int))]
    [TestCase(3.14, typeof(double))]
    [TestCase(true, typeof(bool))]
    public void ConfirmNotType_WhenOfType(object? actual, Type expected)
    {
        Action action = () => actual.ConfirmNotType(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotType failed: "
            + $"Expected object not to be of type {expected.Name}."
        );
    }
    #endregion ConfirmNotType

    #region ConfirmInstanceOf
    [TestCase]
    public void ConfirmInstanceOf_WhenIsInstanceOf()
    {
        _ = true.ConfirmInstanceOf<bool>();
        _ = string.Empty.ConfirmInstanceOf<string>();
        _ = 5f.ConfirmInstanceOf(typeof(float));
    }

    [TestCase]
    public void ConfirmInstanceOf_WhenIsNotInstanceOf()
    {
        Action action = static () => _ = true.ConfirmInstanceOf<string>();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmInstanceOf failed: "
            + "Expected True to be an instance of String."
        );
    }
    #endregion ConfirmInstanceOf

    #region ConfirmNotInstanceOf
    [TestCase]
    public void ConfirmNotInstanceOf_WhenIsNotInstanceOf()
    {
        _ = true.ConfirmNotInstanceOf<string>();
        _ = string.Empty.ConfirmNotInstanceOf<double>();
        _ = 5f.ConfirmNotInstanceOf(typeof(uint));
    }

    [TestCase]
    public void ConfirmNotInstanceOf_WhenIsInstanceOf()
    {
        Action action = static () => _ = true.ConfirmNotInstanceOf<bool>();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotInstanceOf failed: "
            + "Expected True not to be an instance of Boolean."
        );
    }
    #endregion ConfirmNotInstanceOf
}
