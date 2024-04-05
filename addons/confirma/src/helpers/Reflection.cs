using System;
using System.Linq;
using System.Reflection;

namespace Confirma.Helpers;

public static class Reflection
{
	public static Type[] GetClassesFromAssembly(Assembly assembly)
	{
		return assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract).ToArray();
	}
}
