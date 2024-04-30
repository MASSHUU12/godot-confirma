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
		_output = GetNode<RichTextLabel>("%Output");
		Log.RichOutput = _output;

		if (Engine.IsEditorHint())
		{
			_executor = new(new(
				false,
				false,
				false,
				string.Empty
			));
			return;
		}

		_autoload = GetNode<ConfirmaAutoload>("/root/Confirma");
		_executor = new(new(
			_autoload.IsHeadless,
			_autoload.ExitOnFail,
			_autoload.QuitAfterTests,
			_autoload.ClassName
		));
	}

	public void RunAllTests(string className = "")
	{
		_output.Clear();
		_executor.ExecuteTests(_assembly, className);
	}
}
