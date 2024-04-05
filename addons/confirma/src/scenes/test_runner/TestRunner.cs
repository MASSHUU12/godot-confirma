using Godot;

namespace Confirma.TestRunner;

public partial class TestRunner : Control
{
#nullable disable
	private RichTextLabel _output;
#nullable restore

	public override void _Ready()
	{
		_output = GetNode<RichTextLabel>("%Output");
	}
}
