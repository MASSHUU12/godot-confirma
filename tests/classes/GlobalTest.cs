using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Godot;
using static Confirma.Enums.EIgnoreMode;

namespace Confirma.Tests;

[TestClass]
[Ignore(InEditor)]
public class GlobalTest
{
    [TestCase]
    public void RootIsNotNull()
    {
        _ = Global.Root.ConfirmNotNull();
    }

    [TestCase]
    public void RootIsWindow()
    {
        _ = Global.Root.ConfirmType<Window>();
    }

    [TestCase]
    public void RootHasChildren()
    {
        _ = Global.Root.GetChildren().Count.ConfirmIsPositive();
    }
}
