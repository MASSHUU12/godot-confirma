using System;
using System.Collections.Generic;

namespace Confirma.Classes;

public class TestClass
{
	public Type Type { get; }
	public IEnumerable<TestMethod> TestCases { get; }

	public TestClass(Type type)
	{
		Type = type;
		TestCases = TestDiscovery.DiscoverTestMethods(type);
	}
}
