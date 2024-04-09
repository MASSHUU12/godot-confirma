using System.Collections.Generic;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Helpers;

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

	public (uint passed, uint failed) Run(Log log)
	{
		uint passed = 0, failed = 0;

		foreach (var test in TestCases)
		{
			log.Print($"| {Name}{(test.Params.Length > 0 ? $"({test.Params})" : string.Empty)}...");

			try
			{
				test.Run();
				passed++;

				log.PrintSuccess(" passed.\n");
			}
			catch (ConfirmAssertException e)
			{
				failed++;
				log.PrintError($"- Failed: {e.Message}\n");
			}
		}

		return (passed, failed);
	}
}
