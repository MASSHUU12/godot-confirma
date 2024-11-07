using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class HelpPanel : Control
{

#nullable disable
    protected RichTextLabel Output { get; set; }
#nullable restore
    public override void _Ready()
    {
        Output = GetNode<RichTextLabel>("%Output");
        Log.RichOutput = Output;
    }
}
