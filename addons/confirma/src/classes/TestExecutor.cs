using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Confirma.Attributes;
using Confirma.Helpers;
using Confirma.Types;

namespace Confirma.Classes;

public class TestExecutor
{
	private TestResult _result;
	private object _lock = new();

	public TestExecutor()
	{
		_result = new();
	}

	public void ExecuteTests(Assembly assembly, string className)
	{
		var testClasses = TestDiscovery.DiscoverTestClasses(assembly);
		var startTimeStamp = DateTime.Now;

		if (!string.IsNullOrEmpty(className))
		{
			testClasses = testClasses.Where(tc => tc.Type.Name == className);

			if (!testClasses.Any())
			{
				Log.PrintError($"No test class found with the name '{className}'.");
				return;
			}
		}

		ResetStats();

		var parallelizableClasses = testClasses.Where(tc => tc.IsParallelizable);
		var nonParallelizableClasses = testClasses.Where(tc => !tc.IsParallelizable);

		parallelizableClasses.AsParallel().ForAll(ExecuteSingleClass);

		foreach (var testClass in nonParallelizableClasses)
			ExecuteSingleClass(testClass);

		PrintSummary(testClasses.Count(), startTimeStamp);
	}

	private void ExecuteSingleClass(TestClass testClass)
	{
		lock (_lock)
		{
			Log.Print($"> {testClass.Type.Name}...");

			if (testClass.Type.GetCustomAttribute<IgnoreAttribute>() is IgnoreAttribute ignore)
			{
				_result.TestsIgnored += (uint)testClass.TestMethods.Sum(m => m.TestCases.Count());

				Log.PrintWarning($" ignored.\n");
				if (ignore.Reason is not null) Log.PrintWarning($"- {ignore.Reason}\n");
				return;
			}

			Log.PrintLine();

			var classResult = testClass.Run();

			_result.TotalTests += classResult.TestsPassed + classResult.TestsFailed;
			_result.TestsPassed += classResult.TestsPassed;
			_result.TestsFailed += classResult.TestsFailed;
			_result.TestsIgnored += classResult.TestsIgnored;
			_result.Warnings += classResult.Warnings;
		}
	}

	private void PrintSummary(int count, DateTime startTimeStamp)
	{
		Log.PrintLine(
			string.Format(
				"\nConfirma ran {0} tests in {1} test classes. Tests took {2}s.\n{3}, {4}, {5}, {6}.",
				_result.TotalTests,
				count,
				(DateTime.Now - startTimeStamp).TotalSeconds,
				Colors.ColorText($"{_result.TestsPassed} passed", Colors.Success),
				Colors.ColorText($"{_result.TestsFailed} failed", Colors.Error),
				Colors.ColorText($"{_result.TestsIgnored} ignored", Colors.Warning),
				Colors.ColorText($"{_result.Warnings} warnings", Colors.Warning)
			)
		);
	}

	private void ResetStats()
	{
		_result = new();
	}
}
