using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SetUpAttribute : LifecycleAttribute
{
    public SetUpAttribute(string className = "SetUp")
    : base(className) { }
}
