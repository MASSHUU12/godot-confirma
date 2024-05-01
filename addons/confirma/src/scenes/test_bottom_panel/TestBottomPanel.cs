#if TOOLS

using Godot;

namespace Confirma.Scenes;

[Tool]
public partial class TestBottomPanel : Control
{
#nullable disable
	private Button _runAllTests;
	private Button _clearOutput;
	private CheckBox _verbose;
	private TestRunnerEditor _testRunner;
	private ConfirmaAutoload _autoload;
#nullable restore

	public override void _Ready()
	{
		_autoload = GetNode<ConfirmaAutoload>("/root/Confirma");

		_runAllTests = GetNode<Button>("%RunAllTests");
		_runAllTests.Pressed += OnRunAllTestsPressed;

		_clearOutput = GetNode<Button>("%ClearOutput");
		_clearOutput.Pressed += OnClearOutputPressed;

		_verbose = GetNode<CheckBox>("%Verbose");
		_verbose.Toggled += (bool on) => _autoload.Props.IsVerbose = on;

		_testRunner = GetNode<TestRunnerEditor>("%TestRunnerEditor");
	}

	private void OnRunAllTestsPressed()
	{
		_testRunner.RunAllTests();
	}

	private void OnClearOutputPressed()
	{
		_testRunner.ClearOutput();
	}
}

#endif
