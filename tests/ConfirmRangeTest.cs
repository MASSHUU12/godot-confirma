using Confirma.Attributes;
using Confirma.Exceptions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable()]
public static class ConfirmRangeExtension
{
	[TestCase(1, 0, 2)]
	[TestCase(0, 0, 2)]
	[TestCase(2, 0, 2)]
	public static void ConfirmInRange_WhenInRange(int actual, int min, int max)
	{
		actual.ConfirmInRange(min, max);
	}

	[TestCase(0, 1, 2)]
	[TestCase(3, 1, 2)]
	public static void ConfirmInRange_WhenNotInRange(int actual, int min, int max)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmInRange(min, max));
	}

	[TestCase(0, 1, 2)]
	[TestCase(3, 1, 2)]
	public static void ConfirmNotInRange_WhenNotInRange(int actual, int min, int max)
	{
		actual.ConfirmNotInRange(min, max);
	}

	[TestCase(1, 0, 2)]
	[TestCase(0, 0, 2)]
	[TestCase(2, 0, 2)]
	public static void ConfirmNotInRange_WhenInRange(int actual, int min, int max)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmNotInRange(min, max));
	}

	[TestCase(1, 0)]
	[TestCase(0, -1)]
	public static void ConfirmGraterThan_WhenGraterThan(int actual, int value)
	{
		actual.ConfirmGraterThan(value);
	}

	[TestCase(0, 1)]
	[TestCase(-1, 0)]
	public static void ConfirmGraterThan_WhenNotGraterThan(int actual, int value)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmGraterThan(value));
	}

	[TestCase(2, 1)]
	[TestCase(-1, -1)]
	public static void ConfirmGraterThanOrEqual_WhenGraterThanOrEqual(int actual, int value)
	{
		actual.ConfirmGraterThanOrEqual(value);
	}

	[TestCase(0, 1)]
	[TestCase(-1, 0)]
	public static void ConfirmGraterThanOrEqual_WhenNotGraterThanOrEqual(int actual, int value)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmGraterThanOrEqual(value));
	}

	[TestCase(0, 1)]
	[TestCase(-1, 0)]
	public static void ConfirmLessThan_WhenLessThan(int actual, int value)
	{
		actual.ConfirmLessThan(value);
	}

	[TestCase(1, 0)]
	[TestCase(0, -1)]
	public static void ConfirmLessThan_WhenNotLessThan(int actual, int value)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmLessThan(value));
	}

	[TestCase(1, 2)]
	[TestCase(0, 0)]
	public static void ConfirmLessThanOrEqual_WhenLessThanOrEqual(int actual, int value)
	{
		actual.ConfirmLessThanOrEqual(value);
	}

	[TestCase(1, 0)]
	[TestCase(0, -1)]
	public static void ConfirmLessThanOrEqual_WhenNotLessThanOrEqual(int actual, int value)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmLessThanOrEqual(value));
	}
}
