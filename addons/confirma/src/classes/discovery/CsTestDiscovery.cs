using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Helpers;

namespace Confirma.Classes.Discovery;

public static class CsTestDiscovery
{
    public static IEnumerable<Type> GetTestClassesFromAssembly(Assembly assembly)
    {
        return assembly
                .GetTypes()
                .Where(static type => type.HasAttribute<TestClassAttribute>());
    }

    public static IEnumerable<TestingClass> GetParallelTestClasses(
        IEnumerable<TestingClass> classes
    )
    {
        return classes.Where(static tc => tc.IsParallelizable);
    }

    public static IEnumerable<TestingClass> GetSequentialTestClasses(
        IEnumerable<TestingClass> classes
    )
    {
        return classes.Where(static tc => !tc.IsParallelizable);
    }

    public static IEnumerable<MethodInfo> GetTestMethodsFromType(Type type)
    {
        return type.GetMethods().Where(
            static method => method.CustomAttributes.Any(
                static attribute => attribute.AttributeType == typeof(TestCaseAttribute)
            )
        );
    }

    public static IEnumerable<TestingClass> DiscoverTestClasses(Assembly assembly)
    {
        return GetTestClassesFromAssembly(assembly)
            .Select(static testClass => new TestingClass(testClass));
    }

    public static IEnumerable<Attribute> GetTestCasesFromMethod(MethodInfo method)
    {
        return method.GetCustomAttributes().Where(
            static attribute => attribute is TestCaseAttribute or RepeatAttribute
        );
    }

    public static IEnumerable<TestingMethod> DiscoverTestMethods(Type testClass)
    {
        return GetTestMethodsFromType(testClass)
            .Select(static method => new TestingMethod(method));
    }
}
