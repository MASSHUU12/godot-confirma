using System;

namespace Confirma.Fuzz;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class FuzzAttribute : Attribute
{
    public FuzzGenerator Generator { get; init; }

    public FuzzAttribute(
        Type dataType,
        string? name = null,
        float minValue = 0,
        float maxValue = 100,
        EDistributionType distribution = EDistributionType.Uniform,
        int seed = 0 // TODO: Find a better way to recognise lack of value
    )
    {
        Generator = new(
            dataType,
            name,
            minValue,
            maxValue,
            distribution,
            seed == 0 ? null : seed
        );
    }
}
