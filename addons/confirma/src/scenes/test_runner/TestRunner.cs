using System.Reflection;
using Confirma.Classes;
using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class TestRunner : Control
{
#nullable disable
	protected ConfirmaAutoload _autoload;
	protected RichTextLabel _output;
	protected TestExecutor _executor;
#nullable restore

	private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

	public override void _Ready()
	{
		_autoload = GetNode<ConfirmaAutoload>("/root/Confirma");
		_output = GetNode<RichTextLabel>("%Output");
		_executor = new(new(
			_autoload.IsHeadless,
			_autoload.ExitOnFail,
			_autoload.QuitAfterTests,
			_autoload.ClassName
		));

		Log.RichOutput = _output;
	}

	public void RunAllTests(string className = "")
	{
		_output.Clear();
		_executor.ExecuteTests(_assembly, className);
	}
}
