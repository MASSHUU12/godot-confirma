using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class ConfirmaAutoload : Node
{
	public bool RunTests { get; private set; } = false;
	public bool IsHeadless { get; private set; } = false;
	public bool ExitOnFail { get; private set; } = false;
	public bool QuitAfterTests { get; private set; } = false;
	public string ClassName { get; private set; } = string.Empty;

	private const string _testRunnerUID = "uid://cq76c14wl2ti3";
	private const string _paramToRunTests = "--confirma-run";
	private const string _paramQuitAfterTests = "--confirma-quit";
	private const string _paramExitOnFail = "--confirma-exit-on-failure";

	public override void _Ready()
	{
		CheckArguments();

		if (!RunTests) return;

		Log.IsHeadless = IsHeadless;

		ChangeScene();
	}

	private void CheckArguments()
	{
		string[] args = OS.GetCmdlineUserArgs();

		foreach (var arg in args)
		{
			if (!RunTests && arg.StartsWith(_paramToRunTests))
			{
				RunTests = true;

				ClassName = arg.Find('=') == -1
					? string.Empty
					: arg.Split('=')[1];

				continue;
			}

			if (!QuitAfterTests && arg == _paramQuitAfterTests)
			{
				QuitAfterTests = true;
				continue;
			}

			if (!ExitOnFail && arg == _paramExitOnFail)
			{
				ExitOnFail = true;
				continue;
			}
		}

		if (DisplayServer.GetName() == "headless") IsHeadless = true;
	}

	private void ChangeScene()
	{
		GetTree().CallDeferred("change_scene_to_file", _testRunnerUID);

		if (QuitAfterTests) GetTree().Quit();
	}
}
