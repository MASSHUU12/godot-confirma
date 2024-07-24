using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmRangeExtensionTest
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
    public static void ConfirmInRange_WhenNotInRange(int actual, int min, int max)
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => actual.ConfirmInRange(min, max)
        );
    }

    [TestCase(0, 1, 2)]
    [TestCase(3, 1, 2)]
    public static void ConfirmNotInRange_WhenNotInRange(int actual, int min, int max)
    {
        _ = actual.ConfirmNotInRange(min, max);
    }

    [TestCase(1, 0, 2)]
    [TestCase(0, 0, 2)]
    [TestCase(2, 0, 2)]
    public static void ConfirmNotInRange_WhenInRange(int actual, int min, int max)
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => actual.ConfirmNotInRange(min, max)
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
    public static void ConfirmGreaterThan_WhenNotGreaterThan(int actual, int value)
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => actual.ConfirmGreaterThan(value)
        );
    }

    [TestCase(2, 1)]
    [TestCase(-1, -1)]
    public static void ConfirmGreaterThanOrEqual_WhenGreaterThanOrEqual(int actual, int value)
    {
        _ = actual.ConfirmGreaterThanOrEqual(value);
    }

    [TestCase(0, 1)]
    [TestCase(-1, 0)]
    public static void ConfirmGreaterThanOrEqual_WhenNotGreaterThanOrEqual(int actual, int value)
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => actual.ConfirmGreaterThanOrEqual(value)
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
        _ = Confirm.Throws<ConfirmAssertException>(
            () => actual.ConfirmLessThan(value)
        );
    }

    [TestCase(1, 2)]
    [TestCase(0, 0)]
    public static void ConfirmLessThanOrEqual_WhenLessThanOrEqual(int actual, int value)
    {
        _ = actual.ConfirmLessThanOrEqual(value);
    }

    [TestCase(1, 0)]
    [TestCase(0, -1)]
    public static void ConfirmLessThanOrEqual_WhenNotLessThanOrEqual(int actual, int value)
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => actual.ConfirmLessThanOrEqual(value)
        );
    }
}
