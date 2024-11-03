using System;

namespace Confirma.Fuzz;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class FuzzAttribute : Attribute
{
    public FuzzValue Parameters { get; init; }

    public FuzzAttribute(
        Type dataType,
        string? name = null,
        float minValue = 0,
        float maxValue = 100,
        EDistributionType distribution = EDistributionType.Uniform,
        int? seed = null
    )
    {
        Parameters = new(dataType, name, minValue, maxValue, distribution, seed);
    }
}
