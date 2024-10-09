using Godot;

namespace Confirma.Tests;

public partial class ClassToRunTestsOn : CSharpScript
{
    public string Something { get; set; } = "Something";

    public void SomeMethod()
    {
        Something += string.Empty;
    }

    public int Add(int a, int b)
    {
        return a + b;
    }
}
