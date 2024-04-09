#if TOOLS

using Godot;

namespace Confirma.Scenes;

[Tool]
public partial class TestBottomPanel : Control
{
#nullable disable
	private Button _runAllTestsButton;
	private TestRunnerEditor _testRunner;
#nullable restore

	public override void _Ready()
	{
		_runAllTestsButton = GetNode<Button>("%RunAllTests");
		_runAllTestsButton.Pressed += OnRunAllTestsPressed;

		_testRunner = GetNode<TestRunnerEditor>("%TestRunnerEditor");
	}

	private void OnRunAllTestsPressed()
	{
		_testRunner.RunAllTests();
	}
}

#endif
