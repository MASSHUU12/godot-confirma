using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmTest
{
    private enum TestEnum
    {
        A, B, C, D, E
    }

    #region IsEnumValue
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public void IsEnumValue_WhenIsEnumValue(int value)
    {
        _ = Confirm.IsEnumValue<TestEnum>(value);
    }

    [TestCase(-1)]
    [TestCase(5)]
    public void IsEnumValue_WhenISNotEnumValue(int value)
    {
        Action action = () => Confirm.IsEnumValue<TestEnum>(value);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion IsEnumValue

    #region IsNotEnumValue
    [TestCase(-1)]
    [TestCase(5)]
    public void IsNotEnumValue_WhenIsNotEnumValue(int value)
    {
        _ = Confirm.IsNotEnumValue<TestEnum>(value);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public void IsNotEnumValue_WhenIsEnumValue(int value)
    {
        Action action = () => Confirm.IsNotEnumValue<TestEnum>(value);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion IsNotEnumValue

    #region IsEnumName
    [TestCase("A", false)]
    [TestCase("a", true)]
    [TestCase("B", false)]
    [TestCase("b", true)]
    [TestCase("C", false)]
    [TestCase("c", true)]
    [TestCase("D", false)]
    [TestCase("d", true)]
    [TestCase("E", false)]
    [TestCase("e", true)]
    public void IsEnumName_WhenIsEnumName(string name, bool ignoreCase)
    {
        _ = Confirm.IsEnumName<TestEnum>(name, ignoreCase);
    }

    [TestCase("a")]
    [TestCase("b")]
    [TestCase("c")]
    public void IsEnumName_WhenIsEnumNameIncorrectCase(string name)
    {
        Action action = () => Confirm.IsEnumName<TestEnum>(name);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("0", false)]
    [TestCase("F", false)]
    [TestCase("0", true)]
    [TestCase("f", true)]
    public void IsEnumName_WhenIsNotEnumName(string name, bool ignoreCase)
    {
        Action action = () => Confirm.IsEnumName<TestEnum>(name, ignoreCase);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion IsEnumName

    #region IsTrue
    [TestCase]
    public void IsTrue_WhenIsTrue()
    {
        _ = Confirm.IsTrue(true);
    }

    [TestCase]
    public void IsTrue_WhenIsFalse()
    {
        Action action = () => Confirm.IsTrue(false);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion IsTrue

    #region IsFalse
    public void IsFalse_WhenIsFalse()
    {
        _ = Confirm.IsFalse(false);
    }

    [TestCase]
    public void IsFalse_WhenIsTrue()
    {
        Action action = () => Confirm.IsFalse(true);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion IsFalse
}
