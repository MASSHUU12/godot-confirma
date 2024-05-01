using Confirma.Helpers;
using Confirma.Types;
using Godot;

namespace Confirma.Scenes;

[Tool]
public partial class ConfirmaAutoload : Node
{
	public TestsProps Props = new();

	public override void _Ready()
	{
		CheckArguments();

		if (!Props.RunTests) return;

		Log.IsHeadless = Props.IsHeadless;

		ChangeScene();
	}

	private void CheckArguments()
	{
		string[] args = OS.GetCmdlineUserArgs();

		foreach (var arg in args)
		{
			if (!Props.RunTests && arg.StartsWith("--confirma-run"))
			{
				Props.RunTests = true;

				Props.ClassName = arg.Find('=') == -1
					? string.Empty
					: arg.Split('=')[1];

				continue;
			}

			if (!Props.QuitAfterTests && arg == "--confirma-quit")
			{
				Props.QuitAfterTests = true;
				continue;
			}

			if (!Props.ExitOnFail && arg == "--confirma-exit-on-failure")
			{
				Props.ExitOnFail = true;
				continue;
			}

			if (!Props.IsVerbose && arg == "--confirma-verbose")
			{
				Props.IsVerbose = true;
				continue;
			}
		}

		if (DisplayServer.GetName() == "headless") Props.IsHeadless = true;
	}

	private void ChangeScene()
	{
		GetTree().CallDeferred("change_scene_to_file", "uid://cq76c14wl2ti3");

		if (Props.QuitAfterTests) GetTree().Quit();
	}
}
