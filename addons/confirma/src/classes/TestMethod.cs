using System.Collections.Generic;
using System.Reflection;
using Confirma.Attributes;

namespace Confirma.Classes;

public class TestMethod
{
	public MethodInfo Method { get; }
	public IEnumerable<TestCase> TestCases { get; }
	public string Name { get; }

	public TestMethod(MethodInfo method)
	{
		Method = method;
		TestCases = TestDiscovery.DiscoverTestCases(method);
		Name = Method.GetCustomAttribute<TestNameAttribute>()?.Name ?? Method.Name;
	}
}
