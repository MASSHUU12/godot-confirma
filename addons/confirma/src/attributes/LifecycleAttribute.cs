using System;

namespace Confirma.Attributes;

[AttributeUsage(
    AttributeTargets.Class,
    Inherited = true,
    AllowMultiple = false
)]
public class LifecycleAttribute : Attribute
{
    public string ClassName { get; init; }

    public LifecycleAttribute(string className)
    {
        ClassName = className;
    }
}
