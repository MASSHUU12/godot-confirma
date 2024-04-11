using System.Collections.Generic;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Helpers;
using Confirma.Types;

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

	public TestMethodResult Run(Log log)
	{
		uint testsPassed = 0, testsFailed = 0, testsIgnored = 0;

		foreach (TestCase test in TestCases)
		{
			log.Print($"| {Name}{(test.Params.Length > 0 ? $"({test.Params})" : string.Empty)}...");

			if (test.Method.GetCustomAttribute<IgnoreAttribute>() is IgnoreAttribute ignore)
			{
				testsIgnored++;

				log.PrintWarning($" ignored.\n");
				log.PrintWarning($"- {ignore.Reason}\n");
				continue;
			}

			try
			{
				test.Run();
				testsPassed++;

				log.PrintSuccess(" passed.\n");
			}
			catch (ConfirmAssertException e)
			{
				testsFailed++;
				log.PrintError($" failed.\n");
				log.PrintError($"- {e.Message}\n");
			}
		}

		return new(testsPassed, testsFailed, testsIgnored);
	}
}
