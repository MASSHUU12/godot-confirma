using System;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Helpers;
using Confirma.Types;

namespace Confirma.Classes;

public class TestExecutor
{
	private TestResult _result;

	public TestExecutor()
	{
		_result = new(0, 0, 0, 0, 0);
	}

	public void ExecuteTests(Assembly assembly)
	{
		var testClasses = TestDiscovery.DiscoverTestClasses(assembly);
		var count = testClasses.Count();
		var startTimeStamp = DateTime.Now;

		ResetStats();

		foreach (var testClass in testClasses)
		{
			Log.Print($"> {testClass.Type.Name}...");

			if (testClass.Type.GetCustomAttribute<IgnoreAttribute>() is IgnoreAttribute ignore)
			{
				_result.TestsIgnored += (uint)testClass.TestMethods.Sum(m => m.TestCases.Count());

				Log.PrintWarning($" ignored.\n");
				if (ignore.Reason is not null) Log.PrintWarning($"- {ignore.Reason}\n");
				continue;
			}

			Log.PrintLine();

			var classResult = testClass.Run(Log);

			_result.TotalTests += classResult.TestsPassed + classResult.TestsFailed;
			_result.TestsPassed += classResult.TestsPassed;
			_result.TestsFailed += classResult.TestsFailed;
			_result.TestsIgnored += classResult.TestsIgnored;
		}

		Log.PrintLine(
			string.Format(
				"\nConfirma ran {0} tests in {1} test classes. Tests took {2}s. {3}, {4}, {5}.",
				_result.TotalTests,
				count,
				(DateTime.Now - startTimeStamp).TotalSeconds,
				Colors.ColorText($"{_result.TestsPassed} passed", Colors.Success),
				Colors.ColorText($"{_result.TestsFailed} failed", Colors.Error),
				Colors.ColorText($"{_result.TestsIgnored} ignored", Colors.Warning)
			)
		);
	}

	private void ResetStats()
	{
		_result = new(0, 0, 0, 0, 0);
	}
}
