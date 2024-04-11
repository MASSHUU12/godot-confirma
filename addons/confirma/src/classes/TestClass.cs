using System;
using System.Collections.Generic;
using Confirma.Helpers;
using Confirma.Types;

namespace Confirma.Classes;

public class TestClass
{
	public Type Type { get; }
	public IEnumerable<TestMethod> TestMethods { get; }

	public TestClass(Type type)
	{
		Type = type;
		TestMethods = TestDiscovery.DiscoverTestMethods(type);
	}

	public TestClassResult Run(Log log)
	{
		uint passed = 0, failed = 0, ignored = 0;

		foreach (var method in TestMethods)
		{
			var methodResult = method.Run(log);

			passed += methodResult.TestsPassed;
			failed += methodResult.TestsFailed;
			ignored += methodResult.TestsIgnored;
		}

		return new(passed, failed, ignored);
	}
}
