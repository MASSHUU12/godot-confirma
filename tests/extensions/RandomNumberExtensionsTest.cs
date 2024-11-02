using System;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class RandomNumberExtensionsTest
{
    private static readonly Random rg = new();

    [Repeat(3)]
    [TestCase]
    public void NextInt32()
    {
        _ = rg.NextInt32().ConfirmInRange(int.MinValue, int.MaxValue);
    }

    [Repeat(3)]
    [TestCase]
    public void NextDigit()
    {
        _ = rg.NextDigit().ConfirmInRange(0, 9);
    }

    [Repeat(3)]
    [TestCase]
    public void NextNonZeroDigit()
    {
        _ = rg.NextNonZeroDigit().ConfirmInRange(1, 9);
    }

    [Repeat(3)]
    [TestCase]
    public void NextDecimal()
    {
        _ = rg.NextDecimal().ConfirmInRange(decimal.MinValue, decimal.MaxValue);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void NextDecimal_WSign(bool sign)
    {
        _ = rg.NextDecimal(sign).ConfirmSign(sign);
    }

    [Repeat(3)]
    [TestCase]
    public void NextNonNegativeDecimal()
    {
        _ = rg.NextNonNegativeDecimal().ConfirmIsPositive();
    }

    [TestCase(20d)]
    [TestCase(0d)]
    public void NextDecimal_WMaxValue(double maxValue)
    {
        decimal max = (decimal)maxValue;
        _ = rg.NextDecimal(max).ConfirmLessThanOrEqual(max);
    }

    [TestCase(0d, 69d)]
    [TestCase(-10d, 0d)]
    [TestCase(10d, 15d)]
    [TestCase(-15d, -10d)]
    [TestCase(0.1d, 0.2d)]
    public void NextDecimal_WRange(double minValue, double maxValue)
    {
        decimal min = (decimal)minValue;
        decimal max = (decimal)maxValue;

        _ = rg.NextDecimal(min, max).ConfirmInRange(min, max);
    }

    [TestCase]
    public void NextDecimal_WInvalidRange()
    {
        Action action = static () => rg.NextDecimal(2M, 0M);

        _ = action.ConfirmThrows<InvalidOperationException>();
    }

    [Repeat(3)]
    [TestCase]
    public void NextNonNegativeLong()
    {
        _ = rg.NextNonNegativeLong().ConfirmIsPositive();
    }

    [TestCase(20)]
    [TestCase(0)]
    public void NextLong_WMaxValue(long maxValue)
    {
        long max = maxValue;
        _ = rg.NextLong(max).ConfirmLessThanOrEqual(max);
    }

    [TestCase(0, 69)]
    [TestCase(-10, 0)]
    [TestCase(10, 15)]
    [TestCase(-15, -10)]
    [TestCase(long.MinValue, long.MaxValue)]
    public void NextLong_WRange(long min, long max)
    {
        _ = rg.NextLong(min, max).ConfirmInRange(min, max);
    }

    [TestCase]
    public void NextLong_WInvalidRange()
    {
        Action action = static () => rg.NextLong(2, 0);

        _ = action.ConfirmThrows<InvalidOperationException>();
    }

    [TestCase(0d, 69d)]
    [TestCase(-10d, 0d)]
    [TestCase(10d, 15d)]
    [TestCase(-15d, -10d)]
    [TestCase(0.1d, 0.2d)]
    public void NextDouble_WRange(double min, double max)
    {
        _ = rg.NextDouble(min, max).ConfirmInRange(min, max);
    }

    [TestCase]
    public void NextDouble_WInvalidRange()
    {
        Action action = static () => rg.NextDouble(2d, 0d);

        _ = action.ConfirmThrows<InvalidOperationException>();
    }

    #region NextGaussianDouble
    [TestCase]
    public void NextGaussianDouble_MeanIsCorrect()
    {
        const double mean = 10.0;
        const double standardDeviation = 1.0;
        const int numSamples = 100000;

        double sum = 0.0;
        for (int i = 0; i < numSamples; i++)
        {
            sum += rg.NextGaussianDouble(mean, standardDeviation);
        }

        _ = (sum / numSamples).ConfirmCloseTo(mean, 0.1);
    }

    [TestCase]
    public void NextGaussianDouble_StandardDeviationIsCorrect()
    {
        const double mean = 0.0;
        const double standardDeviation = 1.0;
        const int numSamples = 100000;

        double sumOfSquares = 0.0;
        for (int i = 0; i < numSamples; i++)
        {
            double sample = rg.NextGaussianDouble(mean, standardDeviation);
            sumOfSquares += Math.Pow(sample, 2);
        }

        double variance = sumOfSquares / numSamples;
        _ = variance.ConfirmCloseTo(Math.Pow(standardDeviation, 2), 0.1);
    }

    [TestCase]
    public void NextGaussianDouble_DistributionIsSymmetric()
    {
        const double mean = 0.0;
        const double standardDeviation = 1.0;
        const int numSamples = 100000;

        int positiveCount = 0;
        int negativeCount = 0;
        for (int i = 0; i < numSamples; i++)
        {
            double sample = rg.NextGaussianDouble(mean, standardDeviation);
            if (sample > 0)
            {
                positiveCount++;
            }
            else if (sample < 0)
            {
                negativeCount++;
            }
        }

        _ = positiveCount.ConfirmCloseTo(negativeCount, 100);
    }
    #endregion NextGaussianDouble
}
