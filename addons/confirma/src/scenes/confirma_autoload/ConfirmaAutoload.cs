using System.Linq;
using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class ConfirmaAutoload : Node
{
	public bool IsHeadless { get; private set; } = false;
	public bool QuitAfterTests { get; private set; } = false;
	public string ClassName { get; private set; } = string.Empty;

	private const string _testRunnerUID = "uid://cq76c14wl2ti3";
	private const string _paramToRunTests = "--confirma-run";
	private const string _paramQuitAfterTests = "--confirma-quit";

	public override void _Ready()
	{
		var args = OS.GetCmdlineUserArgs();

		if (!args.Any(str => str.StartsWith(_paramToRunTests))) return;

		var keyValue = args.Single(arg => arg.StartsWith(_paramToRunTests));
		ClassName = keyValue.Find('=') == -1
			? string.Empty
			: keyValue.Split('=')[1];

		if (args.Contains(_paramQuitAfterTests)) QuitAfterTests = true;
		if (DisplayServer.GetName() == "headless") IsHeadless = true;

		Log.IsHeadless = IsHeadless;

		RunTests();
	}

	private void RunTests()
	{
		GetTree().CallDeferred("change_scene_to_file", _testRunnerUID);

		if (QuitAfterTests) GetTree().Quit();
	}
}
