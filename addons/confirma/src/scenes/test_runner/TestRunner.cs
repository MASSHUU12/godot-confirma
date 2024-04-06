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

		var assembly = Assembly.GetExecutingAssembly();
		var testClasses = Reflection.GetTestClassesFromAssembly(assembly);

		_output.Text = $"Running {testClasses.Length} tests...\n";

		foreach (var testClass in testClasses)
		{
			var methods = Reflection.GetTestMethodsFromType(testClass);

			_output.AppendText($"Running tests for {testClass.Name}...\n");

			foreach (var method in methods)
			{
				var tests = Reflection.GetTestCasesFromMethod(method);

				foreach (var test in tests)
				{
					_output.AppendText($"\t| Running {method.Name}...\n");

					try
					{
						method.Invoke(null, test.Parameters);
						_output.AppendText("\t| -\t[color=green]Passed.[/color]\n");
					}
					catch (TargetInvocationException e)
					{
						_output.AppendText($"\t| -\t[color=red]Failed: {e.InnerException?.Message}[/color]\n");
					}
				}
			}
		}
	}
}
