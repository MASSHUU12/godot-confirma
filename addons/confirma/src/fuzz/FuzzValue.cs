using System;
using Confirma.Extensions;

namespace Confirma.Fuzz;

public class FuzzValue
{
    public Type DataType { get; init; }
    public string? Name { get; init; }
    public int Min { get; init; }
    public int Max { get; init; }
    public EDistributionType Distribution { get; init; }
    public int? Seed { get; init; }

    private readonly Random _rg;

    public FuzzValue(
        Type dataType,
        string? name,
        int min,
        int max,
        EDistributionType distribution,
        int? seed
    )
    {
        DataType = dataType;
        Name = name;
        Min = min;
        Max = max;
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
            EDistributionType.Gaussian => (int)_rg.NextGaussianDouble(Min, Max),
            EDistributionType.Exponential => (int)_rg.NextExponentialDouble(Min),
            EDistributionType.Poisson => _rg.NextPoissonInt(Min),
            _ => (int)_rg.NextInt64(Min, Max)
        };
    }

    private double NextDouble()
    {
        return Distribution switch
        {
            EDistributionType.Gaussian => _rg.NextGaussianDouble(Min, Max),
            EDistributionType.Exponential => _rg.NextExponentialDouble(Min),
            EDistributionType.Poisson => _rg.NextPoissonInt(Min),
            _ => _rg.NextDouble(Min, Max)
        };
    }

    private string NextString()
    {
        return _rg.NextString(Min, Max);
    }
}
