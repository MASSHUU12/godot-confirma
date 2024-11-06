using System;

namespace Confirma.Fuzz;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class FuzzAttribute : Attribute
{
    public FuzzGenerator Generator { get; init; }

    public FuzzAttribute(
        Type dataType,
        string? name = null,
        double min = 0,
        double max = 100,
        double mean = 1,
        double standardDeviation = 1,
        double lambda = 1,
        EDistributionType distribution = EDistributionType.Uniform,
        int seed = 0 // TODO: Find a better way to recognise lack of value
    )
    {
        Generator = new(
            dataType: dataType,
            name: name,
            min: min,
            max: max,
            mean: mean,
            standardDeviation: standardDeviation,
            lambda: lambda,
            distribution: distribution,
            seed: seed == 0 ? null : seed
        );
    }
}
