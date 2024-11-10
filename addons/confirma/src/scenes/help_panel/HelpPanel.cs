using Confirma.Classes;
using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class HelpPanel : Control
{
    public override void _Ready()
    {
        Log.RichOutput = GetNode<RichTextLabel>("%Output");
        Log.RichOutput.MetaClicked += _on_meta_clicked;

        ConfirmaAutoload autoload = GetNodeOrNull<ConfirmaAutoload>("/root/Confirma");

        Help.ShowHelpPage(autoload.Props.SelectedHelpPage);

    }

    public void _on_meta_clicked (Variant meta)
    {
        switch(meta.VariantType)
        {
            case Variant.Type.String:
                OS.ShellOpen((string)meta);
                break;
            //place for future expansion
        }
    }
}
