using System.Collections.Generic;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Exceptions;
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

	public TestMethodResult Run(TestsProps props)
	{
		uint testsPassed = 0, testsFailed = 0, testsIgnored = 0;

		foreach (TestCase test in TestCases)
		{
			Log.Print($"| {Name}{(test.Params.Length > 0 ? $"({test.Params})" : string.Empty)}...");

			if (test.Method.GetCustomAttribute<IgnoreAttribute>() is IgnoreAttribute ignore)
			{
				testsIgnored++;

				Log.PrintWarning($" ignored.\n");
				if (ignore.Reason is not null) Log.PrintWarning($"- {ignore.Reason}\n");
				continue;
			}

			try
			{
				test.Run();
				testsPassed++;

				Log.PrintSuccess(" passed.\n");
			}
			catch (ConfirmAssertException e)
			{
				testsFailed++;

				Log.PrintError($" failed.\n");
				Log.PrintError($"- {e.Message}\n");

				if (props.ExitOnFail) props.CallExitOnFailure();
			}
		}

		return new(testsPassed, testsFailed, testsIgnored);
	}
}
