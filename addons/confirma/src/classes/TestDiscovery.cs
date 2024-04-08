using System;
using System.Collections.Generic;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Helpers;

namespace Confirma.Classes;

public class TestDiscovery
{
	public static IEnumerable<Type> DiscoverTestClasses(Assembly assembly)
	{
		return Reflection.GetTestClassesFromAssembly(assembly);
	}

	public static IEnumerable<MethodInfo> DiscoverTestMethods(Type testClass)
	{
		return Reflection.GetTestMethodsFromType(testClass);
	}

	public static IEnumerable<TestCaseAttribute> DiscoverTestCases(MethodInfo method)
	{
		return Reflection.GetTestCasesFromMethod(method);
	}
}
