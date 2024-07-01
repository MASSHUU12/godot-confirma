using System;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomBooleanTest
{
	private readonly static Random rg = new();

	[Repeat(5)]
	[TestCase]
	public static void NextBool()
	{
		const uint ITERATIONS = 100000;
		uint trueCount = 0;

		for (int i = 0; i < ITERATIONS; i++) if (rg.NextBool()) trueCount++;

		var truePercentage = (double)trueCount / ITERATIONS * 100;
		truePercentage.ConfirmCloseTo(50, 1);
	}

	[Repeat(5)]
	[TestCase]
	public static void NextNullableBool()
	{
		const uint ITERATIONS = 100000;

		uint trueCount = 0;
		uint falseCount = 0;
		uint nullCount = 0;

		for (int i = 0; i < ITERATIONS; i++)
		{
			bool? result = rg.NextNullableBool();

			if (result == true) trueCount++;
			else if (result == false) falseCount++;
			else nullCount++;
		}

		var truePercentage = (double)trueCount / ITERATIONS * 100;
		var falsePercentage = (double)falseCount / ITERATIONS * 100;
		var nullPercentage = (double)nullCount / ITERATIONS * 100;

		nullPercentage.ConfirmCloseTo(33.33, 1);
		truePercentage.ConfirmCloseTo(33.33, 1);
		falsePercentage.ConfirmCloseTo(33.33, 1);
	}
}
