using System;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomBooleanTest
{
    private static readonly Random rg = new();

    [Repeat(5)]
    [TestCase]
    public static void NextBool()
    {
        const uint ITERATIONS = 100000;
        uint trueCount = 0;

        for (int i = 0; i < ITERATIONS; i++)
        {
            if (rg.NextBool())
            {
                trueCount++;
            }
        }

        double truePercentage = (double)trueCount / ITERATIONS * 100;
        _ = truePercentage.ConfirmCloseTo(50, 1);
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

            if (result == true)
            {
                trueCount++;
            }
            else if (result == false)
            {
                falseCount++;
            }
            else
            {
                nullCount++;
            }
        }

        double truePercentage = (double)trueCount / ITERATIONS * 100;
        double falsePercentage = (double)falseCount / ITERATIONS * 100;
        double nullPercentage = (double)nullCount / ITERATIONS * 100;

        _ = nullPercentage.ConfirmCloseTo(33.33, 1);
        _ = truePercentage.ConfirmCloseTo(33.33, 1);
        _ = falsePercentage.ConfirmCloseTo(33.33, 1);
    }
}
