using System;

namespace Confirma.Attributes;

[AttributeUsage(
    AttributeTargets.Class,
    Inherited = true,
    AllowMultiple = false
)]
public class LifecycleAttribute : Attribute
{
    public string MethodName { get; init; }

    public LifecycleAttribute(string className)
    {
        MethodName = className;
    }
}
