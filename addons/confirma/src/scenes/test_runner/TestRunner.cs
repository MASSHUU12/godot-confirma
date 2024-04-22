using System.Reflection;
using System.Threading.Tasks;
using Confirma.Classes;
using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class TestRunner : Control
{
#nullable disable
	protected RichTextLabel _output;
	protected TestExecutor _executor;
#nullable restore

	private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

	public override void _Ready()
	{
		_output = GetNode<RichTextLabel>("%Output");
		_executor = new();

		Log.RichOutput = _output;
	}

	public void RunAllTests(string className = "")
	{
		_output.Clear();
		Task.Run(
			async () => await _executor.ExecuteTestsAsync(_assembly, className)
		).GetAwaiter().GetResult();
	}
}
