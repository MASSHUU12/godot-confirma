using System;
using Confirma.Enums;
using Confirma.Types;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class FuzzAttribute : Attribute
{
    public FuzzParameter Parameters { get; init; }

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
