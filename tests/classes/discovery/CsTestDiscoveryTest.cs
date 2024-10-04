using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Classes.Discovery;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class CsTestDiscoveryTest
{
    private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

    [Parallelizable]
    private class ParallelizableTestClass { }

    private static class TestClass
    {
        [TestCase]
        public static void TestMethod() { }

        [TestCase]
        public static void TestMethod2() { }

        public static void NonTestMethod() { }
    }

    [TestCase]
    public static void GetTestClassesFromAssembly_ReturnsTestClasses()
    {
        IEnumerable<Type> testClasses = CsTestDiscovery
            .GetTestClassesFromAssembly(_assembly);

        _ = testClasses.ConfirmCountGreaterThan(0);
    }

    [TestCase]
    public static void GetTestClassesFromAssembly_DoesNotReturnNonTestClass()
    {
        IEnumerable<Type> testClasses = CsTestDiscovery
            .GetTestClassesFromAssembly(_assembly);

        _ = testClasses.ConfirmAllMatch(
            static tc => tc.HasAttribute<TestClassAttribute>()
        );
    }

    [TestCase]
    public static void GetParallelTestClasses_ReturnsParallelizableClasses()
    {
        TestingClass[] classes = new[]
        {
            new TestingClass(typeof(ParallelizableTestClass)),
            new(typeof(TestClass))
        };

        IEnumerable<TestingClass> parallelClasses = CsTestDiscovery
            .GetParallelTestClasses(classes);

        _ = parallelClasses.ConfirmCount(1);
        _ = parallelClasses.First().IsParallelizable.ConfirmTrue();
    }

    [TestCase]
    public static void GetSequentialTestClasses_ReturnsSequentialClasses()
    {
        TestingClass[] classes = new[]
        {
            new TestingClass(typeof(ParallelizableTestClass)),
            new(typeof(TestClass))
        };

        IEnumerable<TestingClass> parallelClasses = CsTestDiscovery
            .GetSequentialTestClasses(classes);

        _ = parallelClasses.ConfirmCount(1);
        _ = parallelClasses.First().IsParallelizable.ConfirmFalse();
    }

    [TestCase]
    public static void GetTestMethodsFromType_ReturnsTestMethods()
    {
        IEnumerable<MethodInfo> testMethods = CsTestDiscovery
            .GetTestMethodsFromType(typeof(TestClass));

        _ = testMethods.ConfirmCount(2);
    }

    [TestCase]
    public static void GetTestMethodsFromType_DoesNotReturnNonTestMethod()
    {
        Type type = typeof(TestClass);
        MethodInfo? nonTestMethod = type.GetMethod("NonTestMethod");

        IEnumerable<MethodInfo> testMethods = CsTestDiscovery
            .GetTestMethodsFromType(type);

        _ = testMethods.ConfirmNotContains(nonTestMethod);
    }

    [TestCase]
    public static void DiscoverTestClasses_ReturnsTestClasses()
    {
        IEnumerable<TestingClass> testClasses = CsTestDiscovery
            .DiscoverTestClasses(_assembly);

        _ = testClasses.ConfirmNotEmpty();
    }

    [TestCase]
    public static void GetTestCasesFromMethod_ReturnsTestCases()
    {
        MethodInfo method = typeof(CsTestDiscoveryTest)
            .GetMethod("GetTestCasesFromMethod_ReturnsTestCases")!;

        IEnumerable<Attribute> testCases = CsTestDiscovery
            .GetTestCasesFromMethod(method);

        _ = testCases.ConfirmCount(1);
    }

    [TestCase]
    public static void DiscoverTestMethods_ReturnsTestMethods()
    {
        IEnumerable<TestingMethod> testMethods = CsTestDiscovery
            .DiscoverTestMethods(typeof(CsTestDiscoveryTest));

        _ = testMethods.ConfirmCountGreaterThan(0);
    }
}
