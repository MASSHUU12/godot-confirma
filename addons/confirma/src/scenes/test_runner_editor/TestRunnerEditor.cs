#if TOOLS

using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

[Tool]
public partial class TestRunnerEditor : TestRunner
{
	public override void _Ready()
	{
		base._Ready();

		_executor = new(new Log(_output), new(false));
	}
}

#endif
