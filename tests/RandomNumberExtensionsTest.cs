using System;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomNumberExtensionsTest
{
	private static readonly Random rg = new();

	[TestCase]
	public static void NextInt32()
	{
		rg.NextInt32().ConfirmInRange(int.MinValue, int.MaxValue);
		rg.NextInt32().ConfirmInRange(int.MinValue, int.MaxValue);
		rg.NextInt32().ConfirmInRange(int.MinValue, int.MaxValue);
	}

	[TestCase]
	public static void NextDigit()
	{
		rg.NextDigit().ConfirmInRange(0, 9);
		rg.NextDigit().ConfirmInRange(0, 9);
		rg.NextDigit().ConfirmInRange(0, 9);
	}

	[TestCase]
	public static void NextNonZeroDigit()
	{
		rg.NextNonZeroDigit().ConfirmInRange(1, 9);
		rg.NextNonZeroDigit().ConfirmInRange(1, 9);
		rg.NextNonZeroDigit().ConfirmInRange(1, 9);
	}

	[TestCase]
	public static void NextDecimal()
	{
		rg.NextDecimal().ConfirmInRange(decimal.MinValue, decimal.MaxValue);
		rg.NextDecimal().ConfirmInRange(decimal.MinValue, decimal.MaxValue);
		rg.NextDecimal().ConfirmInRange(decimal.MinValue, decimal.MaxValue);
	}

	[TestCase(true)]
	[TestCase(false)]
	public static void NextDecimal_WSign(bool sign)
	{
		rg.NextDecimal(sign).ConfirmSign(sign);
	}

	[TestCase]
	public static void NextNonNegativeDecimal()
	{
		rg.NextNonNegativeDecimal().ConfirmIsPositive();
		rg.NextNonNegativeDecimal().ConfirmIsPositive();
		rg.NextNonNegativeDecimal().ConfirmIsPositive();
	}

	[TestCase(20d)]
	[TestCase(0d)]
	public static void NextDecimal_WMaxValue(double maxValue)
	{
		decimal max = (decimal)maxValue;
		rg.NextDecimal(max).ConfirmLessThanOrEqual(max);
	}

	[TestCase(0d, 69d)]
	[TestCase(-10d, 0d)]
	[TestCase(10d, 15d)]
	[TestCase(-15d, -10d)]
	[TestCase(0.1d, 0.2d)]
	public static void NextDecimal_WRange(double minValue, double maxValue)
	{
		decimal min = (decimal)minValue;
		decimal max = (decimal)maxValue;

		rg.NextDecimal(min, max).ConfirmInRange(min, max);
	}

	[TestCase]
	public static void NextDecimal_WInvalidRange()
	{
		Action action = () => rg.NextDecimal(2M, 0M);

		action.ConfirmThrows<InvalidOperationException>();
	}

	[TestCase]
	public static void NextNonNegativeLong()
	{
		rg.NextNonNegativeLong().ConfirmIsPositive();
		rg.NextNonNegativeLong().ConfirmIsPositive();
		rg.NextNonNegativeLong().ConfirmIsPositive();
	}

	[TestCase(20)]
	[TestCase(0)]
	public static void NextLong_WMaxValue(long maxValue)
	{
		long max = (long)maxValue;
		rg.NextLong(max).ConfirmLessThanOrEqual(max);
	}

	[TestCase(0, 69)]
	[TestCase(-10, 0)]
	[TestCase(10, 15)]
	[TestCase(-15, -10)]
	[TestCase(long.MinValue, long.MaxValue)]
	public static void NextLong_WRange(long min, long max)
	{
		rg.NextLong(min, max).ConfirmInRange(min, max);
	}

	[TestCase]
	public static void NextLong_WInvalidRange()
	{
		Action action = () => rg.NextLong(2, 0);

		action.ConfirmThrows<InvalidOperationException>();
	}
}
