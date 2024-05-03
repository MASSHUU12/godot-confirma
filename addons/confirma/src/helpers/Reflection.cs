using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes;

namespace Confirma.Helpers;

public static class Reflection
{
	public static Type[] GetClassesFromAssembly(Assembly assembly)
	{
		return assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract).ToArray();
	}

	public static Type[] GetTestClassesFromAssembly(Assembly assembly)
	{
		var types = assembly.GetTypes();
		return types.Where(
			type => type.HasAttribute<TestClassAttribute>() ||
					type.IsSubclassOf(typeof(TestClass))
		).ToArray();
	}

	public static IEnumerable<TestingClass> GetParallelTestClasses(IEnumerable<TestingClass> classes)
	{
		return classes.Where(tc => tc.IsParallelizable);
	}

	public static IEnumerable<TestingClass> GetSequentialTestClasses(IEnumerable<TestingClass> classes)
	{
		return classes.Where(tc => !tc.IsParallelizable);
	}

	public static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(Type type)
	where T : Attribute
	{
		return type.GetMethods().Where(method => method.CustomAttributes.Any(
			attribute => attribute.AttributeType == typeof(T)
		));
	}

	public static MethodInfo[] GetTestMethodsFromType(Type type)
	{
		return type.GetMethods().Where(method => method.CustomAttributes.Any(
			attribute => attribute.AttributeType == typeof(TestCaseAttribute)
		)).ToArray();
	}

	public static bool HasAttribute<T>(this object obj) where T : Attribute
	{
		return Attribute.GetCustomAttribute(obj.GetType(), typeof(T)) is not null;
	}

	public static bool HasAttribute<T>(this Type obj) where T : Attribute
	{
		return Attribute.GetCustomAttribute(obj, typeof(T)) is not null;
	}

	public static TestCaseAttribute[] GetTestCasesFromMethod(MethodInfo method)
	{
		return method.GetCustomAttributes<TestCaseAttribute>().ToArray();
	}
}
