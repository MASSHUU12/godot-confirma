using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmNumericTest
{
    #region ConfirmIsPositive
    [TestCase(1f)]
    [TestCase(0.1f)]
    public static void ConfirmIsPositive_WhenPositive(float actual)
    {
        _ = actual.ConfirmIsPositive();
    }

    [TestCase(0f)]
    [TestCase(-1f)]
    public static void ConfirmIsPositive_WhenNotPositive(float actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmIsPositive());
    }
    #endregion ConfirmIsPositive

    #region ConfirmIsNotPositive
    [TestCase(0f)]
    [TestCase(-1f)]
    public static void ConfirmIsNotPositive_WhenNotPositive(float actual)
    {
        _ = actual.ConfirmIsNotPositive();
    }

    [TestCase(1f)]
    [TestCase(0.1f)]
    public static void ConfirmIsNotPositive_WhenPositive(float actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmIsNotPositive());
    }
    #endregion ConfirmIsNotPositive

    #region ConfirmIsNegative
    [TestCase(-1f)]
    [TestCase(-0.1f)]
    public static void ConfirmIsNegative_WhenNegative(float actual)
    {
        _ = actual.ConfirmIsNegative();
    }

    [TestCase(0f)]
    [TestCase(1f)]
    public static void ConfirmIsNegative_WhenNotNegative(float actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmIsNegative());
    }
    #endregion ConfirmIsNegative

    #region ConfirmIsNotNegative
    [TestCase(0f)]
    [TestCase(1f)]
    public static void ConfirmIsNotNegative_WhenNotNegative(float actual)
    {
        _ = actual.ConfirmIsNotNegative();
    }

    [TestCase(-1f)]
    [TestCase(-0.1f)]
    public static void ConfirmIsNotNegative_WhenNegative(float actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmIsNotNegative());
    }
    #endregion ConfirmIsNotNegative

    #region ConfirmSign
    [TestCase(-5, true)]
    [TestCase(5, false)]
    public static void ConfirmSign_WhenCorrect(int number, bool expected)
    {
        _ = number.ConfirmSign(expected);
    }

    [TestCase(-5, false)]
    [TestCase(5, true)]
    public static void ConfirmSign_WhenIncorrect(int number, bool expected)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => number.ConfirmSign(expected));
    }
    #endregion ConfirmSign

    #region ConfirmIsZero
    [TestCase(0f)]
    public static void ConfirmIsZero_WhenZero(float actual)
    {
        _ = actual.ConfirmIsZero();
    }

    [TestCase(1f)]
    [TestCase(-1f)]
    public static void ConfirmIsZero_WhenNotZero(float actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmIsZero());
    }
    #endregion ConfirmIsZero

    #region ConfirmIsOdd
    [TestCase(1)]
    [TestCase(69)]
    [TestCase(2137)]
    public static void ConfirmIsOdd_WhenIsOdd(int actual)
    {
        _ = actual.ConfirmIsOdd();
    }

    [TestCase(2)]
    [TestCase(70)]
    [TestCase(2138)]
    public static void ConfirmIsOdd_WhenIsEven(int actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmIsOdd());
    }
    #endregion ConfirmIsOdd

    #region ConfirmIsEven
    [TestCase(2)]
    [TestCase(70)]
    [TestCase(2138)]
    public static void ConfirmIsEven_WhenIsEven(int actual)
    {
        _ = actual.ConfirmIsEven();
    }

    [TestCase(1)]
    [TestCase(69)]
    [TestCase(2137)]
    public static void ConfirmIsEven_WhenIsOdd(int actual)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => actual.ConfirmIsEven());
    }
    #endregion ConfirmIsEven

    #region ConfirmCloseTo
    [TestCase(5f, 6f, 2f)]
    [TestCase(5f, 5.1f, 0.1f)]
    [TestCase(6f, 5f, 2f)]
    [TestCase(5.1f, 5f, 0.1f)]
    [TestCase(-5f, -4.5f, 0.5f)]
    public static void ConfirmCloseTo_WhenCloseTo(
        float actual,
        float expected,
        float tolerance
    )
    {
        _ = actual.ConfirmCloseTo(expected, tolerance);
    }

    [TestCase(5d, 0d, 1d)]
    [TestCase(5d, 15d, 1d)]
    [TestCase(0d, 0.1d, 0.01d)]
    public static void ConfirmCloseTo_WhenNotCloseTo(
        double actual,
        double expected,
        double tolerance
    )
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => actual.ConfirmCloseTo(expected, tolerance)
        );
    }
    #endregion ConfirmCloseTo
}
