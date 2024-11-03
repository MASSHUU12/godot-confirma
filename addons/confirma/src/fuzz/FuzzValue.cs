using System;

namespace Confirma.Fuzz;

public class FuzzValue
{
    public Type DataType { get; init; }
    public string? Name { get; init; }
    public int Min { get; init; }
    public int Max { get; init; }
    public EDistributionType Distribution { get; init; }

    public FuzzValue(
        Type dataType,
        string? name,
        int min,
        int max,
        EDistributionType distribution
    )
    {
        DataType = dataType;
        Name = name;
        Min = min;
        Max = max;
        Distribution = distribution;
    }
};
