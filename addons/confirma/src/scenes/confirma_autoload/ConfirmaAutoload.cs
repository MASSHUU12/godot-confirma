using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class ConfirmaAutoload : Node
{
	public bool RunTests { get; private set; } = false;
	public bool IsHeadless { get; private set; } = false;
	public bool ExitOnFail { get; private set; } = false;
	public bool VerboseOutput { get; private set; } = false;
	public bool QuitAfterTests { get; private set; } = false;
	public string ClassName { get; private set; } = string.Empty;

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
			if (!RunTests && arg.StartsWith("--confirma-run"))
			{
				RunTests = true;

				ClassName = arg.Find('=') == -1
					? string.Empty
					: arg.Split('=')[1];

				continue;
			}

			if (!QuitAfterTests && arg == "--confirma-quit")
			{
				QuitAfterTests = true;
				continue;
			}

			if (!ExitOnFail && arg == "--confirma-exit-on-failure")
			{
				ExitOnFail = true;
				continue;
			}

			if (!VerboseOutput && arg == "--confirma-verbose")
			{
				VerboseOutput = true;
				continue;
			}
		}

		if (DisplayServer.GetName() == "headless") IsHeadless = true;
	}

	private void ChangeScene()
	{
		GetTree().CallDeferred("change_scene_to_file", "uid://cq76c14wl2ti3");

		if (QuitAfterTests) GetTree().Quit();
	}
}
