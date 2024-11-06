using System;
using Confirma.Extensions;

namespace Confirma.Fuzz;

public class FuzzGenerator
{
    public Type DataType { get; init; }
    public string? Name { get; init; }
    public double Min { get; init; }
    public double Max { get; init; }
    public double Mean { get; init; }
    public double StandardDeviation { get; init; }
    public double Lambda { get; init; }
    public EDistributionType Distribution { get; init; }
    public int? Seed { get; init; }

    private readonly Random _rg;

    public FuzzGenerator(
        Type dataType,
        string? name,
        double min,
        double max,
        double mean,
        double standardDeviation,
        double lambda,
        EDistributionType distribution,
        int? seed
    )
    {
        DataType = dataType;
        Name = name;
        Min = min;
        Max = max;
        Mean = mean;
        StandardDeviation = standardDeviation;
        Lambda = lambda;
        Distribution = distribution;
        Seed = seed;

        _rg = seed.HasValue ? new(seed.Value) : new();
    }

    public object NextValue()
    {
        switch (DataType)
        {
            case Type t when t == typeof(int):
                return NextInt();
            case Type t when t == typeof(double):
                return NextDouble();
            case Type t when t == typeof(float):
                return (float)NextDouble();
            case Type t when t == typeof(string):
                return NextString();
            case Type t when t == typeof(bool):
                return _rg.NextBool();
            default:
                throw new ArgumentException($"{DataType.Name} is unsupported.");
        }
    }

    private int NextInt()
    {
        return Distribution switch
        {
            EDistributionType.Gaussian => (int)_rg.NextGaussianDouble(
                Mean,
                StandardDeviation
            ),
            EDistributionType.Exponential => (int)_rg.NextExponentialDouble(
                Lambda
            ),
            EDistributionType.Poisson => _rg.NextPoissonInt(Lambda),
            _ => (int)_rg.NextInt64((int)Min, (int)Max)
        };
    }

    private double NextDouble()
    {
        return Distribution switch
        {
            EDistributionType.Gaussian => _rg.NextGaussianDouble(
                Mean,
                StandardDeviation
            ),
            EDistributionType.Exponential => _rg.NextExponentialDouble(Lambda),
            EDistributionType.Poisson => _rg.NextPoissonInt(Lambda),
            _ => _rg.NextDouble(Min, Max)
        };
    }

    private string NextString()
    {
        return _rg.NextString((int)Min, (int)Max);
    }
}
