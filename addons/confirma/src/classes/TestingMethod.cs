using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Helpers;
using Confirma.Types;

using static Confirma.Enums.ETestCaseState;

namespace Confirma.Classes;

public class TestingMethod
{
	public MethodInfo Method { get; }
	public IEnumerable<TestCase> TestCases { get; }
	public string Name { get; }
	public TestMethodResult Result { get; private set; }

	public TestingMethod(MethodInfo method)
	{
		Method = method;
		TestCases = DiscoverTestCases();
		Name = Method.GetCustomAttribute<TestNameAttribute>()?.Name ?? Method.Name;
		Result = new();
	}

	public TestMethodResult Run(TestsProps props)
	{
		uint testsPassed = 0, testsFailed = 0, testsIgnored = 0;

		foreach (TestCase test in TestCases)
		{
			var attr = test.Method.GetCustomAttribute<IgnoreAttribute>();
			if (attr is not null && attr.IsIgnored())
			{
				testsIgnored++;

				TestOutput.PrintOutput(Name, test.Params, Ignored, props.IsVerbose, attr.Reason);
				continue;
			}

			try
			{
				test.Run();
				testsPassed++;

				TestOutput.PrintOutput(Name, test.Params, Passed, props.IsVerbose);
			}
			catch (ConfirmAssertException e)
			{
				testsFailed++;

				TestOutput.PrintOutput(Name, test.Params, Failed, props.IsVerbose, e.Message);

				if (props.ExitOnFail) props.CallExitOnFailure();
			}
		}

		return new(testsPassed, testsFailed, testsIgnored);
	}

	private IEnumerable<TestCase> DiscoverTestCases()
	{
		List<TestCase> cases = new();
		var discovered = TestDiscovery.GetTestCasesFromMethod(Method).GetEnumerator();

		while (discovered.MoveNext())
		{
			if (discovered.Current is TestCaseAttribute testCase)
			{
				cases.Add(new(Method, testCase.Parameters, 0));
			}

			if (discovered.Current is RepeatAttribute repeat)
			{
				if (!discovered.MoveNext())
				{
					Log.PrintError(
						$"The Repeat attribute for the \"{Method.Name}\" method will be ignored " +
						"because it does not have the TestCase attribute after it."
					);
					continue;
				}

				if (discovered.Current is RepeatAttribute)
				{
					Log.PrintError("Repeat attributes cannot occur in succession.");
					continue;
				}

				if (discovered.Current is not TestCaseAttribute tc) continue;

				cases.Add(new(Method, tc.Parameters, repeat.Repeat));
			}
		}

		return cases.AsEnumerable();
	}
}
