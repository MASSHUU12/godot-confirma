using Confirma.Classes;
using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class HelpPanel : Control
{
    public override void _Ready()
    {
        Log.RichOutput = GetNode<RichTextLabel>("%Output");

        ConfirmaAutoload autoload = GetNodeOrNull<ConfirmaAutoload>("/root/Confirma");

        Help.ShowHelpPage(autoload.Props.SelectedHelpPage);

    }
}
