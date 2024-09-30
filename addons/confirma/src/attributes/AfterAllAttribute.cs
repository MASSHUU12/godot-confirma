using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AfterAllAttribute : LifecycleAttribute
{
    public AfterAllAttribute(string methodName = "AfterAll")
    : base(methodName) { }
}
