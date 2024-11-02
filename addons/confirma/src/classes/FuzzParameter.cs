using System;
using Confirma.Enums;

namespace Confirma.Classes;

public class FuzzParameter
{
    public string? Name { get; init; }
    public Type DataType { get; init; }
    public int MinValue { get; init; }
    public int MaxValue { get; init; }
    public EDistributionType Distribution { get; init; }

    public FuzzParameter(
        Type dataType,
        string? name = null,
        int minValue = 0,
        int maxValue = 100,
        EDistributionType distribution = EDistributionType.Uniform
    )
    {
        Name = name;
        DataType = dataType;
        MinValue = minValue;
        MaxValue = maxValue;
        Distribution = distribution;
    }
}
