using Godot;

namespace Confirma.Classes;

public abstract class TestClass
{
	public readonly Window Root;

	public TestClass(Window root)
	{
		Root = root;
	}
}
