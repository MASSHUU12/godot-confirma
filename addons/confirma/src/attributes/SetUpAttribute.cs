using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SetUpAttribute : LifecycleAttribute
{
    public SetUpAttribute(string methodName = "SetUp")
    : base(methodName) { }
}
