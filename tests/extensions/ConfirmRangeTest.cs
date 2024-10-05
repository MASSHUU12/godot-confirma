using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmRangeTest
{
    [TestCase(1, 0, 2)]
    [TestCase(0, 0, 2)]
    [TestCase(2, 0, 2)]
    public static void ConfirmInRange_WhenInRange(int actual, int min, int max)
    {
        _ = actual.ConfirmInRange(min, max);
    }

    [TestCase(0, 1, 2)]
    [TestCase(3, 1, 2)]
    public static void ConfirmInRange_WhenNotInRange(
        int actual,
        int min,
        int max
    )
    {
        Action action = () => actual.ConfirmInRange(min, max);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmInRange failed: "
            + $"Expected {actual} to be within the range [{min}, {max}]."
        );
    }

    [TestCase(0, 1, 2)]
    [TestCase(3, 1, 2)]
    public static void ConfirmNotInRange_WhenNotInRange(
        int actual,
        int min,
        int max
    )
    {
        _ = actual.ConfirmNotInRange(min, max);
    }

    [TestCase(1, 0, 2)]
    [TestCase(0, 0, 2)]
    [TestCase(2, 0, 2)]
    public static void ConfirmNotInRange_WhenInRange(
        int actual,
        int min,
        int max
    )
    {
        Action action = () => actual.ConfirmNotInRange(min, max);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotInRange failed: "
            + $"Expected {actual} to be outside the range [{min}, {max}]."
        );
    }

    [TestCase(1, 0)]
    [TestCase(0, -1)]
    public static void ConfirmGreaterThan_WhenGreaterThan(int actual, int value)
    {
        _ = actual.ConfirmGreaterThan(value);
    }

    [TestCase(0, 1)]
    [TestCase(-1, 0)]
    public static void ConfirmGreaterThan_WhenNotGreaterThan(
        int actual,
        int value
    )
    {
        Action action = () => actual.ConfirmGreaterThan(value);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmGreaterThan failed: "
            + $"Expected {actual} to be greater than {value}."
        );
    }

    [TestCase(2, 1)]
    [TestCase(-1, -1)]
    public static void ConfirmGreaterThanOrEqual_WhenGreaterThanOrEqual(
        int actual,
        int value
    )
    {
        _ = actual.ConfirmGreaterThanOrEqual(value);
    }

    [TestCase(0, 1)]
    [TestCase(-1, 0)]
    public static void ConfirmGreaterThanOrEqual_WhenNotGreaterThanOrEqual(
        int actual,
        int value
    )
    {
        Action action = () => actual.ConfirmGreaterThanOrEqual(value);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmGreaterThanOrEqual failed: "
            + $"Expected {actual} to be greater than or equal {value}."
        );
    }

    [TestCase(0, 1)]
    [TestCase(-1, 0)]
    public static void ConfirmLessThan_WhenLessThan(int actual, int value)
    {
        _ = actual.ConfirmLessThan(value);
    }

    [TestCase(1, 0)]
    [TestCase(0, -1)]
    public static void ConfirmLessThan_WhenNotLessThan(int actual, int value)
    {
        Action action = () => actual.ConfirmLessThan(value);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmLessThan failed: "
            + $"Expected {actual} to be less than {value}."
        );
    }

    [TestCase(1, 2)]
    [TestCase(0, 0)]
    public static void ConfirmLessThanOrEqual_WhenLessThanOrEqual(
        int actual,
        int value
    )
    {
        _ = actual.ConfirmLessThanOrEqual(value);
    }

    [TestCase(1, 0)]
    [TestCase(0, -1)]
    public static void ConfirmLessThanOrEqual_WhenNotLessThanOrEqual(
        int actual,
        int value
    )
    {
        Action action = () => actual.ConfirmLessThanOrEqual(value);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmLessThanOrEqual failed: "
            + $"Expected {actual} to be less than or equal {value}."
        );
    }
}
