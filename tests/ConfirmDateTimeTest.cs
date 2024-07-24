using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmDateTimeTest
{
    #region ConfirmIsBefore
    [TestCase(5, 15)]
    [TestCase(0, 15)]
    [TestCase(2137, 6969)]
    public static void ConfirmIsBefore_WhenIsBefore(long actualTicks, long dateTicks)
    {
        _ = new DateTime(actualTicks).ConfirmIsBefore(new(dateTicks));
    }

    [TestCase(15, 15)]
    [TestCase(15, 0)]
    [TestCase(6969, 2137)]
    public static void ConfirmIsBefore_WhenIsNotBefore(long actualTicks, long dateTicks)
    {
        Action action = () => new DateTime(actualTicks).ConfirmIsBefore(new(dateTicks));

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmIsBefore

    #region ConfirmIsOnOrBefore
    [TestCase(0, 0)]
    [TestCase(5, 15)]
    [TestCase(2137, 2137)]
    [TestCase(2137, 6969)]
    public static void ConfirmIsOnOrBefore_WhenIsOnOrBefore(long actualTicks, long dateTicks)
    {
        _ = new DateTime(actualTicks).ConfirmIsOnOrBefore(new(dateTicks));
    }

    [TestCase(15, 0)]
    [TestCase(6969, 2137)]
    public static void ConfirmIsOnOrBefore_WhenIsNotOnOrBefore(long actualTicks, long dateTicks)
    {
        Action action = () => new DateTime(actualTicks).ConfirmIsOnOrBefore(new(dateTicks));

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmIsOnOrBefore

    #region ConfirmIsAfter
    [TestCase(15, 5)]
    [TestCase(15, 0)]
    [TestCase(6969, 2137)]
    public static void ConfirmIsAfter_WhenIsAfter(long actualTicks, long dateTicks)
    {
        _ = new DateTime(actualTicks).ConfirmIsAfter(new(dateTicks));
    }

    [TestCase(15, 15)]
    [TestCase(0, 15)]
    [TestCase(2137, 6969)]
    public static void ConfirmIsAfter_WhenIsNotAfter(long actualTicks, long dateTicks)
    {
        Action action = () => new DateTime(actualTicks).ConfirmIsAfter(new(dateTicks));

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmIsAfter

    #region ConfirmIsOnOrAfter
    [TestCase(0, 0)]
    [TestCase(15, 5)]
    [TestCase(2137, 2137)]
    [TestCase(6969, 2137)]
    public static void ConfirmIsOnOrAfter_WhenIsOnOrAfter(long actualTicks, long dateTicks)
    {
        _ = new DateTime(actualTicks).ConfirmIsOnOrAfter(new(dateTicks));
    }

    [TestCase(0, 15)]
    [TestCase(2137, 6969)]
    public static void ConfirmIsOnOrAfter_WhenIsNotOnOrAfter(long actualTicks, long dateTicks)
    {
        Action action = () => new DateTime(actualTicks).ConfirmIsOnOrAfter(new(dateTicks));

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmIsOnOrAfter
}
