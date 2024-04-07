using System.Linq;
using Godot;

namespace Confirma.Scenes;

public partial class ConfirmaAutoload : Node
{
	private const string _testRunnerUID = "uid://cqbaquyf403ot";
	private const string _paramToRunTests = "--confirma-run";

	public override void _Ready()
	{
		if (!OS.GetCmdlineUserArgs().Contains(_paramToRunTests)) return;

		GetTree().CallDeferred("change_scene_to_file", _testRunnerUID);
	}
}
