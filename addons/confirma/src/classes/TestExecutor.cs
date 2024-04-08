using System.Linq;
using System.Reflection;
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

	private void ExecuteTestClass(TestClass testClass)
	{
		_log.PrintLine($"Running {testClass.Type.Name}...");

		foreach (var method in testClass.TestMethods) RunTestMethod(method);
	}

	private void RunTestMethod(TestMethod method)
	{
		_testCount += (uint)method.TestCases.Count();

		foreach (var test in method.TestCases)
		{
			_log.Print($"| Running {method.Name}{(
				test.Params.Length > 0
				? $"({test.Params})"
				: string.Empty)}..."
			);

			try
			{
				test.Run();
				_passed++;

				_log.PrintSuccess(" passed.\n");
			}
			catch (ConfirmAssertException e)
			{
				_failed++;
				_log.PrintError($"- Failed: {e.Message}\n");
			}
		}
	}
}
