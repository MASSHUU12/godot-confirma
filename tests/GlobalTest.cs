using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Godot;
using static Confirma.Enums.EIgnoreMode;

namespace Confirma.Tests;

[TestClass]
[Ignore(InEditor)]
public static class GlobalTest
{
    [TestCase]
    public static void RootIsNotNull()
    {
        _ = Global.Root.ConfirmNotNull();
    }

    [TestCase]
    public static void RootIsWindow()
    {
        _ = Global.Root.ConfirmType<Window>();
    }

    [TestCase]
    public static void RootHasChildren()
    {
        _ = Global.Root.GetChildren().Count.ConfirmIsPositive();
    }
}
