using System;

namespace Confirma.Fuzz;

public readonly record struct FuzzParameter(
    Type DataType,
    string? Name = null,
    int MinValue = 0,
    int MaxValue = 100,
    EDistributionType Distribution = EDistributionType.Uniform
);
