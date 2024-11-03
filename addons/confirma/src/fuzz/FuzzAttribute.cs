using System;

namespace Confirma.Fuzz;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class FuzzAttribute : Attribute
{
    public FuzzValue Parameters { get; init; }

    public FuzzAttribute(
        Type dataType,
        string? name = null,
        int minValue = 0,
        int maxValue = 100,
        EDistributionType distribution = EDistributionType.Uniform
    )
    {
        Parameters = new(dataType, name, minValue, maxValue, distribution);
    }
}
