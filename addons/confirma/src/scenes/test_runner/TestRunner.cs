using System;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class TestRunner : Control
{
#nullable disable
	private Log _log;
	private RichTextLabel _output;
	private ConfirmaAutoload _confirmaAutoload;
	private Helpers.Colors _colors;
#nullable restore

	private uint _testCount, _passed, _failed;

	public override void _Ready()
	{
		_output = GetNode<RichTextLabel>("%Output");
		_confirmaAutoload = GetNode<ConfirmaAutoload>("/root/Confirma");

		_colors = new(_confirmaAutoload.IsHeadless);
		_log = new Log(_confirmaAutoload.IsHeadless ? null : _output);

		RunTests(Assembly.GetExecutingAssembly());
	}

	public void RunTests(Assembly assembly)
	{
		var testClasses = Reflection.GetTestClassesFromAssembly(assembly);
		_log.PrintLine($"Detected {testClasses.Length} test classes...");

		foreach (var testClass in testClasses) RunTestClass(testClass);

		_log.PrintLine(
			string.Format(
				"\nConfirma ran {0} tests in {1} test classes. {2}, {3}.",
				_testCount,
				testClasses.Length,
				_colors.Auto($"{_passed} passed", "green"),
				_colors.Auto($"{_failed} failed", "red")
			)
		);
	}

	private void RunTestClass(Type testClass)
	{
		var methods = Reflection.GetTestMethodsFromType(testClass);
		_log.PrintLine($"Running {testClass.Name}...");

		foreach (var method in methods) RunTestMethod(method);
	}

	private void RunTestMethod(MethodInfo method)
	{
		var tests = Reflection.GetTestCasesFromMethod(method);
		var testName = method.GetCustomAttribute<TestNameAttribute>()?.Name ?? method.Name;

		_testCount += (uint)tests.Length;

		foreach (var test in tests)
		{
			var strParams = string.Join(", ", test.Parameters ?? Array.Empty<object>());
			_log.Print($"| Running {testName}({strParams})...");

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
