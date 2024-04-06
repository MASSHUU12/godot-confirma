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

		foreach (var test in tests)
		{
			var strParams = string.Join(", ", test.Parameters);
			_output.AppendText($"\t| Running {method.Name}({strParams})...");

			try
			{
				method.Invoke(null, test.Parameters);
				_output.AppendText(" [color=green]passed.[/color]\n");
			}
			catch (TargetInvocationException tie)
			{
				_output.AppendText($"\n\t| -\t[color=red]Failed: {tie.InnerException?.Message}[/color]\n");
			}
			catch (ArgumentException)
			{
				_output.AppendText($"\n\t| -\t[color=red]Failed: Invalid test case parameters: {strParams}.[/color]\n");
			}
			catch (Exception e)
			{
				_output.AppendText($"\n\t| -\t[color=red]Failed: {e.Message}[/color]\n");
			}
		}
	}
}
