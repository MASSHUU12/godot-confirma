using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TearDownAttribute : LifecycleAttribute
{
    public TearDownAttribute(string methodName = "TearDown")
    : base(methodName) { }
}
