using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Helpers;

namespace Confirma.Classes;

public class TestExecutor
{
	private readonly Log _log;
	private readonly Colors _colors;

	private uint _testCount, _passed, _failed;

	public TestExecutor(Log log, Colors colors)
	{
		_log = log;
		_colors = colors;
	}

	public void ExecuteTests(Assembly assembly)
	{
		var testClasses = TestDiscovery.DiscoverTestClasses(assembly);
		var count = testClasses.Count();

		_log.PrintLine($"Detected {count} test classes...");

		foreach (var testClass in testClasses) ExecuteTestClass(testClass);

		_log.PrintLine(
			string.Format(
				"\nConfirma ran {0} tests in {1} test classes. {2}, {3}.",
				_testCount,
				count,
				_colors.Auto($"{_passed} passed", "green"),
				_colors.Auto($"{_failed} failed", "red")
			)
		);
	}

	private void ExecuteTestClass(Type testClass)
	{
		var methods = TestDiscovery.DiscoverTestMethods(testClass);
		_log.PrintLine($"Running {testClass.Name}...");

		foreach (var method in methods) RunTestMethod(method);
	}

	private void RunTestMethod(MethodInfo method)
	{
		var tests = TestDiscovery.DiscoverTestCases(method).ToArray();
		var testName = method.GetCustomAttribute<TestNameAttribute>()?.Name ?? method.Name;

		_testCount += (uint)tests.Length;

		foreach (var test in tests)
		{
			var strParams = string.Join(", ", test.Parameters ?? Array.Empty<object>());
			_log.Print($"| Running {testName}{(strParams.Length > 0 ? $"({strParams})" : string.Empty)}...");

			try
			{
				method.Invoke(null, test.Parameters);
				_passed++;

				_log.PrintSuccess(" passed.\n");
			}
			catch (TargetInvocationException tie)
			{
				_failed++;
				_log.PrintError($"- Failed: {tie.InnerException?.Message}\n");
			}
			catch (Exception e) when (e
				is ArgumentException
				or ArgumentNullException
			)
			{
				_failed++;
				_log.PrintError($"- Failed: Invalid test case parameters: {strParams}.\n");
			}
			catch (Exception e)
			{
				_failed++;
				_log.PrintError($"- Failed: {e.Message}\n");
			}
		}
	}
}
