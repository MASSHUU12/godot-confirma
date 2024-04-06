using System;
using System.Reflection;
using Confirma.Helpers;
using Godot;

namespace Confirma.TestRunner;

public partial class TestRunner : Control
{
#nullable disable
	private RichTextLabel _output;
#nullable restore

	private uint _testCount, _passed, _failed;

	public override void _Ready()
	{
		_output = GetNode<RichTextLabel>("%Output");

		RunTests(Assembly.GetExecutingAssembly());
	}

	public void RunTests(Assembly assembly)
	{
		var testClasses = Reflection.GetTestClassesFromAssembly(assembly);
		_output.Text = $"Running {testClasses.Length} tests...\n";

		foreach (var testClass in testClasses) RunTestClass(testClass);

		_output.AppendText(
			string.Format(
				"\nConfirma ran {0} tests. [color=green]{1} passed[/color], [color=red]{2} failed[/color].",
				_testCount,
				_passed,
				_failed)
		);
	}

	private void RunTestClass(Type testClass)
	{
		var methods = Reflection.GetTestMethodsFromType(testClass);
		_output.AppendText($"Running {testClass.Name}...\n");

		foreach (var method in methods) RunTestMethod(method);
	}

	private void RunTestMethod(MethodInfo method)
	{
		var tests = Reflection.GetTestCasesFromMethod(method);

		_testCount += (uint)tests.Length;

		foreach (var test in tests)
		{
			var strParams = string.Join(", ", test.Parameters);
			_output.AppendText($"\t| Running {method.Name}({strParams})...");

			try
			{
				method.Invoke(null, test.Parameters);
				_passed++;

				_output.AppendText(" [color=green]passed.[/color]\n");
			}
			catch (TargetInvocationException tie)
			{
				_failed++;
				_output.AppendText($"\n\t| -\t[color=red]Failed: {tie.InnerException?.Message}[/color]\n");
			}
			catch (ArgumentException)
			{
				_failed++;
				_output.AppendText($"\n\t| -\t[color=red]Failed: Invalid test case parameters: {strParams}.[/color]\n");
			}
			catch (Exception e)
			{
				_failed++;
				_output.AppendText($"\n\t| -\t[color=red]Failed: {e.Message}[/color]\n");
			}
		}
	}
}
