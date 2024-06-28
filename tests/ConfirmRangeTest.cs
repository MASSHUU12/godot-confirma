using System;
using Confirma.Attributes;
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
		actual.ConfirmInRange(min, max);
	}

	[TestCase(0, 1, 2)]
	[TestCase(3, 1, 2)]
	public static void ConfirmInRange_WhenNotInRange(int actual, int min, int max)
	{
		Action action = () => actual.ConfirmInRange(min, max);

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => actual.ConfirmNotInRange(min, max);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	[TestCase(1, 0)]
	[TestCase(0, -1)]
	public static void ConfirmGreaterThan_WhenGreaterThan(int actual, int value)
	{
		actual.ConfirmGreaterThan(value);
	}

	[TestCase(0, 1)]
	[TestCase(-1, 0)]
	public static void ConfirmGreaterThan_WhenNotGreaterThan(int actual, int value)
	{
		Action action = () => actual.ConfirmGreaterThan(value);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	[TestCase(2, 1)]
	[TestCase(-1, -1)]
	public static void ConfirmGreaterThanOrEqual_WhenGreaterThanOrEqual(int actual, int value)
	{
		actual.ConfirmGreaterThanOrEqual(value);
	}

	[TestCase(0, 1)]
	[TestCase(-1, 0)]
	public static void ConfirmGreaterThanOrEqual_WhenNotGreaterThanOrEqual(int actual, int value)
	{
		Action action = () => actual.ConfirmGreaterThanOrEqual(value);

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => actual.ConfirmLessThan(value);

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => actual.ConfirmLessThanOrEqual(value);

		action.ConfirmThrows<ConfirmAssertException>();
	}
}
