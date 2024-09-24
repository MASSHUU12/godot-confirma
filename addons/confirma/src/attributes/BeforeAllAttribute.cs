using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class BeforeAllAttribute : LifecycleAttribute
{
    public BeforeAllAttribute(string methodName = "BeforeAll")
    : base(methodName) { }
}
