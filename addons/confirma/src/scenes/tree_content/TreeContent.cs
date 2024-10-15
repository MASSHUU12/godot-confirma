#if TOOLS
using Godot;
using static Godot.TreeItem;

namespace Confirma.Scenes;

[Tool]
public partial class TreeContent : Tree
{
    public TreeItem AddRoot()
    {
        return CreateItem();
    }

    public TreeItem AddLabel(string text, TreeItem? parent = null)
    {
        TreeItem child = CreateItem(parent);
        child.SetText(0, text);
        child.SetSelectable(0, false);
        child.SetSelectable(1, false);

        return child;
    }

    public TreeItem AddCheckBox(string text, TreeItem? parent = null)
    {
        TreeItem child = CreateItem(parent);
        child.SetText(0, text);
        child.SetEditable(1, true);
        child.SetCellMode(1, TreeCellMode.Check);
        child.SetSelectable(0, false);
        child.SetSelectable(1, false);
        // child.SetExpandRight(0, true);
        // child.SetExpandRight(1, false);

        return child;
    }
}
#endif
