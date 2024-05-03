using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
public static class GlobalTest
{
	[TestCase]
	public static void RootIsNotNull()
	{
		Global.Root.ConfirmNotNull();
	}

	[TestCase]
	public static void RootIsWindow()
	{
		Global.Root.ConfirmType<Window>();
	}

	[TestCase]
	public static void RootHasChildren()
	{
		Global.Root.GetChildren().Count.ConfirmIsPositive();
	}
}
