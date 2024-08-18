using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ColorsTest
{
    [TestCase]
    public static void ColorText_WithHexColorCode_ReturnsColoredText()
    {
        string result = Colors.ColorText("Hello, World!", "#FF0000");

        _ = result.ConfirmEqual(
            Log.IsHeadless
                ? "\x1b[38;2;255;0;0mHello, World!\x1b[0m"
                : "[color=#ff0000ff]Hello, World![/color]"
        );
    }

    [TestCase]
    public static void ColorText_WithColorObject_ReturnsColoredText()
    {
        string result = Colors.ColorText("Hello, World!", new Godot.Color(1, 0, 0));

        _ = result.ConfirmEqual(
            Log.IsHeadless
                ? "\x1b[38;2;255;0;0mHello, World!\x1b[0m"
                : "[color=#ff0000ff]Hello, World![/color]"
        );
    }

    [TestCase("#Invalid")]
    [TestCase("#1234567")]
    public static void ColorText_WithInvalidColorCode_ThrowsException(
        string invalidColor
    )
    {
        const string text = "Hello, World!";

        _ = Confirm.Throws<ArgumentOutOfRangeException>(
            () => Colors.ColorText(text, invalidColor)
        );
    }

    [TestCase]
    public static void ToTerminal_WithColor_ReturnsColoredText()
    {
        string result = Colors.ToTerminal("Hello, World!", new Godot.Color(1, 0, 0));

        _ = result.ConfirmEqual("\x1b[38;2;255;0;0mHello, World!\x1b[0m");
    }

    [TestCase]
    public static void ToGodot_WithColor_ReturnsColoredText()
    {
        string result = Colors.ToGodot("Hello, World!", new Godot.Color(1, 0, 0));

        _ = result.ConfirmEqual("[color=#ff0000ff]Hello, World![/color]");
    }
}
