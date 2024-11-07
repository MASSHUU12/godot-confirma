using System.Reflection;
using Confirma.Helpers;
using Confirma.Attributes;
using System.Collections.Generic;
using System;
using Confirma.Extensions;
using Confirma.Classes;
using System.Linq;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ReflectionTest
{
    [TestCase]
    public void GetClassesFromAssembly_ReturnsOnlyConcreteClasses()
    {
        Assembly assembly = typeof(ReflectionTest).Assembly;
        IEnumerable<Type> classes = Reflection.GetClassesFromAssembly(assembly);

        _ = classes.ConfirmNotEmpty();
        _ = Confirm.IsTrue(
            classes.All(static c => c.IsClass && !c.IsAbstract)
        );
    }

    [TestCase]
    public void GetMethodsWithAttribute_ReturnsMethodsMarkedWithAttribute()
    {
        IEnumerable<MethodInfo> methods = Reflection
            .GetMethodsWithAttribute<TestCaseAttribute>(typeof(ReflectionTest));

        _ = methods.ConfirmNotEmpty();
        _ = Confirm.IsTrue(
            methods.All(
                static m => m.CustomAttributes.Any(
                    static a => a.AttributeType == typeof(TestCaseAttribute)
                )
            )
        );
    }

    [TestCase]
    public void GetMethodsWithAttribute_ReturnsNoMethodsWhenNoAttribute()
    {
        IEnumerable<MethodInfo> methods = Reflection
            .GetMethodsWithAttribute<IgnoreAttribute>(typeof(ReflectionTest));

        _ = methods.ConfirmEmpty();
    }

    [TestCase]
    public void HasAttribute_ReturnsTrueForObjectWithAttribute()
    {
        bool result = new IgnoreAttribute().HasAttribute<AttributeUsageAttribute>();

        _ = result.ConfirmTrue();
    }

    [TestCase]
    public void HasAttribute_ReturnsFalseForObjectWithoutAttribute()
    {
        bool result = new IgnoreAttribute().HasAttribute<TestCaseAttribute>();

        _ = result.ConfirmFalse();
    }

    [TestCase]
    public void HasAttribute_ReturnsTrueForTypeWithAttribute()
    {
        bool result = typeof(ReflectionTest).HasAttribute<TestClassAttribute>();

        _ = result.ConfirmTrue();
    }

    [TestCase]
    public void HasAttribute_ReturnsFalseForTypeWithoutAttribute()
    {
        bool result = typeof(ReflectionTest).HasAttribute<IgnoreAttribute>();

        _ = result.ConfirmFalse();
    }
}
