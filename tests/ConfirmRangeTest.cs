using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmRangeExtension
{
	[TestCase(1, 0, 2)]
	[TestCase(0, 0, 2)]
	[TestCase(2, 0, 2)]
	public static void ConfirmInRange_WhenInRange(int actual, int min, int max)
	{
		actual.ConfirmInRange(min, max);
	}

	// TODO: Create a test case for ConfirmInRange_WhenNotInRange

	[TestCase(0, 1, 2)]
	[TestCase(3, 1, 2)]
	public static void ConfirmNotInRange_WhenNotInRange(int actual, int min, int max)
	{
		actual.ConfirmNotInRange(min, max);
	}

	// TODO: Create a test case for ConfirmNotInRange_WhenInRange

	[TestCase(1, 0)]
	[TestCase(0, -1)]
	public static void ConfirmGraterThan_WhenGraterThan(int actual, int value)
	{
		actual.ConfirmGraterThan(value);
	}

	// TODO: Create a test case for ConfirmGraterThan_WhenNotGraterThan

	[TestCase(2, 1)]
	[TestCase(-1, -1)]
	public static void ConfirmGraterThanOrEqual_WhenGraterThanOrEqual(int actual, int value)
	{
		actual.ConfirmGraterThanOrEqual(value);
	}

	// TODO: Create a test case for ConfirmGraterThanOrEqual_WhenNotGraterThanOrEqual

	[TestCase(0, 1)]
	[TestCase(-1, 0)]
	public static void ConfirmLessThan_WhenLessThan(int actual, int value)
	{
		actual.ConfirmLessThan(value);
	}

	// TODO: Create a test case for ConfirmLessThan_WhenNotLessThan

	[TestCase(1, 2)]
	[TestCase(0, 0)]
	public static void ConfirmLessThanOrEqual_WhenLessThanOrEqual(int actual, int value)
	{
		actual.ConfirmLessThanOrEqual(value);
	}

	// TODO: Create a test case for ConfirmLessThanOrEqual_WhenNotLessThanOrEqual
}
