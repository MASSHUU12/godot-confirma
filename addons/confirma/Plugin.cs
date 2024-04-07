#if TOOLS
using Godot;

namespace Confirma;

[Tool]
public partial class Plugin : EditorPlugin
{
	public override void _EnterTree()
	{
		AddAutoloadSingleton("Confirma", "res://addons/confirma/src/scenes/confirma_autoload/ConfirmaAutoload.tscn");
	}

	public override void _ExitTree()
	{
		RemoveAutoloadSingleton("Confirma");
	}
}
#endif
