using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmNumericTest
{
    #region ConfirmIsPositive
    [TestCase(1f)]
    [TestCase(0.1f)]
    public void ConfirmIsPositive_WhenPositive(float actual)
    {
        _ = actual.ConfirmIsPositive();
    }

    [TestCase(0f, "0.00000")]
    [TestCase(-1f, "-1.00000")]
    public void ConfirmIsPositive_WhenNotPositive(
        float actual,
        string formatted
    )
    {
        Action action = () => actual.ConfirmIsPositive();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsPositive failed: "
            + $"Expected {formatted} to be positive."
        );
    }
    #endregion ConfirmIsPositive

    #region ConfirmIsNotPositive
    [TestCase(0f)]
    [TestCase(-1f)]
    public void ConfirmIsNotPositive_WhenNotPositive(float actual)
    {
        _ = actual.ConfirmIsNotPositive();
    }

    [TestCase(1f, "1.00000")]
    [TestCase(0.1f, "0.10000")]
    public void ConfirmIsNotPositive_WhenPositive(
        float actual,
        string formatted
    )
    {
        Action action = () => actual.ConfirmIsNotPositive();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNotPositive failed: "
            + $"Expected {formatted} to be not positive."
        );
    }
    #endregion ConfirmIsNotPositive

    #region ConfirmIsNegative
    [TestCase(-1f)]
    [TestCase(-0.1f)]
    public void ConfirmIsNegative_WhenNegative(float actual)
    {
        _ = actual.ConfirmIsNegative();
    }

    [TestCase(0f, "0.00000")]
    [TestCase(1f, "1.00000")]
    public void ConfirmIsNegative_WhenNotNegative(
        float actual,
        string formatted
    )
    {
        Action action = () => actual.ConfirmIsNegative();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNegative failed: "
            + $"Expected {formatted} to be negative."
        );
    }
    #endregion ConfirmIsNegative

    #region ConfirmIsNotNegative
    [TestCase(0f)]
    [TestCase(1f)]
    public void ConfirmIsNotNegative_WhenNotNegative(float actual)
    {
        _ = actual.ConfirmIsNotNegative();
    }

    [TestCase(-1f, "-1.00000")]
    [TestCase(-0.1f, "-0.10000")]
    public void ConfirmIsNotNegative_WhenNegative(
        float actual,
        string formatted
    )
    {
        Action action = () => actual.ConfirmIsNotNegative();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNotNegative failed: "
            + $"Expected {formatted} to be not negative."
        );
    }
    #endregion ConfirmIsNotNegative

    #region ConfirmSign
    [TestCase(-5, true)]
    [TestCase(5, false)]
    public void ConfirmSign_WhenCorrect(int number, bool expected)
    {
        _ = number.ConfirmSign(expected);
    }

    [TestCase(-5, false)]
    [TestCase(5, true)]
    public void ConfirmSign_WhenIncorrect(int actual, bool expected)
    {
        Action action = () => actual.ConfirmSign(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmSign failed: "
            + $"Expected {actual} to have a "
            + $"{(expected ? "negative" : "positive")} sign."
        );
    }
    #endregion ConfirmSign

    #region ConfirmIsZero
    [TestCase(0f)]
    public void ConfirmIsZero_WhenZero(float actual)
    {
        _ = actual.ConfirmIsZero();
    }

    [TestCase(1f, "1.00000")]
    [TestCase(-1f, "-1.00000")]
    public void ConfirmIsZero_WhenNotZero(
        float actual,
        string formatted
    )
    {
        Action action = () => actual.ConfirmIsZero();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsZero failed: "
            + $"Expected {formatted} to be zero."
        );
    }
    #endregion ConfirmIsZero

    #region ConfirmIsNotZero
    [TestCase(1f)]
    [TestCase(-1f)]
    public void ConfirmIsNotZero_WhenNotZero(float actual)
    {
        _ = actual.ConfirmIsNotZero();
    }

    [TestCase(0)]
    public void ConfirmIsNotZero_WhenZero(int actual)
    {
        Action action = () => actual.ConfirmIsNotZero();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNotZero failed: "
            + $"Expected {actual} to be not zero."
        );
    }
    #endregion ConfirmIsNotZero

    #region ConfirmIsOdd
    [TestCase(1)]
    [TestCase(69)]
    [TestCase(421)]
    public void ConfirmIsOdd_WhenIsOdd(int actual)
    {
        _ = actual.ConfirmIsOdd();
    }

    [TestCase(2)]
    [TestCase(70)]
    [TestCase(420)]
    public void ConfirmIsOdd_WhenIsEven(int actual)
    {
        Action action = () => actual.ConfirmIsOdd();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsOdd failed: "
            + $"Expected {actual} to be odd."
        );
    }
    #endregion ConfirmIsOdd

    #region ConfirmIsEven
    [TestCase(2)]
    [TestCase(70)]
    [TestCase(420)]
    public void ConfirmIsEven_WhenIsEven(int actual)
    {
        _ = actual.ConfirmIsEven();
    }

    [TestCase(1)]
    [TestCase(69)]
    [TestCase(421)]
    public void ConfirmIsEven_WhenIsOdd(int actual)
    {
        Action action = () => actual.ConfirmIsEven();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsEven failed: "
            + $"Expected {actual} to be even."
        );
    }
    #endregion ConfirmIsEven

    #region ConfirmCloseTo
    [TestCase(5f, 6f, 2f)]
    [TestCase(5f, 5.1f, 0.1f)]
    [TestCase(6f, 5f, 2f)]
    [TestCase(5.1f, 5f, 0.1f)]
    [TestCase(-5f, -4.5f, 0.5f)]
    public void ConfirmCloseTo_WhenCloseTo(
        float actual,
        float expected,
        float tolerance
    )
    {
        _ = actual.ConfirmCloseTo(expected, tolerance);
    }

    [TestCase(5d, "5.00000", 0d, "0.00000", 1d)]
    [TestCase(5d, "5.00000", 15d, "15.00000", 1d)]
    [TestCase(0d, "0.00000", 0.1d, "0.10000", 0.01d)]
    public void ConfirmCloseTo_WhenNotCloseTo(
        double actual,
        string aFormatted,
        double expected,
        string eFormatted,
        double tolerance
    )
    {
        Action action = () => actual.ConfirmCloseTo(expected, tolerance);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmCloseTo failed: "
            + $"Expected {aFormatted} to be close to {eFormatted}."
        );
    }
    #endregion ConfirmCloseTo

    #region ConfirmIsNaN
    [TestCase]
    public void ConfirmIsNaN_WhenIsNaN()
    {
        _ = double.NaN.ConfirmIsNaN();
    }

    [TestCase]
    public void ConfirmIsNaN_WhenIsNotNaN()
    {
        Action action = static () => 5d.ConfirmIsNaN();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNaN failed: Expected 5.00000 to be NaN."
        );
    }
    #endregion ConfirmIsNaN

    #region ConfirmIsNotNaN
    [TestCase]
    public void ConfirmIsNotNaN_WhenIsNotNaN()
    {
        _ = 5d.ConfirmIsNotNaN();
    }

    [TestCase]
    public void ConfirmIsNotNaN_WhenIsNaN()
    {
        Action action = static () => double.NaN.ConfirmIsNotNaN();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNotNaN failed: Expected value not to be NaN."
        );
    }
    #endregion ConfirmIsNotNaN
}
