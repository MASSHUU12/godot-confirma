using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ColorsTest
{
    [TestCase]
    public void Color_BothNull_ReturnsClearText()
    {
        string result = Colors.Color("Some Text");

        _ = result.ConfirmEqual(result);
    }

    [TestCase]
    public void Color_BothHex_ReturnsColoredBoth()
    {
        string result = Colors.Color("Some Text", "#FF0000","#FF0000");

        _ = result.ConfirmEqual(
            Log.IsHeadless
                ? "\x1b[48;2;255;0;0m\x1b[38;2;255;0;0mSome Text\x1b[0m"
                : "[bgcolor=#ff0000ff][color=#ff0000ff]Some Text[/color][/bgcolor]"
        );
    }

    [TestCase]
    public void Color_BackgroundHex_TextNull_ReturnsColoredTextBackground()
    {
        string result = Colors.Color("Some Text", BgColor: "#FF0000");

        _ = result.ConfirmEqual(
            Log.IsHeadless
                ? "\x1b[48;2;255;0;0mSome Text\x1b[0m"
                : "[bgcolor=#ff0000ff]Some Text[/bgcolor]"
        );
    }

    [TestCase]
    public void Color_BackgroundNull_TextHex_ReturnsColoredText()
    {
        string result = Colors.Color("Some Text", TextColor: "#FF0000");

        _ = result.ConfirmEqual(
            Log.IsHeadless
                ? "\x1b[38;2;255;0;0mSome Text\x1b[0m"
                : "[color=#ff0000ff]Some Text[/color]"
        );
    }
    [TestCase]
    public void ColorText_WithHexColorCode_ReturnsColoredText()
    {
        string result = Colors.ColorText("Hello, World!", "#FF0000");

        _ = result.ConfirmEqual(
            Log.IsHeadless
                ? "\x1b[38;2;255;0;0mHello, World!\x1b[0m"
                : "[color=#ff0000ff]Hello, World![/color]"
        );
    }

    [TestCase]
    public void ColorText_WithColorObject_ReturnsColoredText()
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
    public void ColorText_WithInvalidColorCode_ThrowsException(
        string invalidColor
    )
    {
        const string text = "Hello, World!";

        _ = Confirm.Throws<ArgumentOutOfRangeException>(
            () => Colors.ColorText(text, invalidColor)
        );
    }

    [TestCase]
    public void TextToTerminal_WithColor_ReturnsColoredText()
    {
        string result = Colors.TextToTerminal("Hello, World!", new Godot.Color(1, 0, 0));

        _ = result.ConfirmEqual("\x1b[38;2;255;0;0mHello, World!\x1b[0m");
    }

    [TestCase]
    public void TextToGodot_WithColor_ReturnsColoredText()
    {
        string result = Colors.TextToGodot("Hello, World!", new Godot.Color(1, 0, 0));

        _ = result.ConfirmEqual("[color=#ff0000ff]Hello, World![/color]");
    }

    [TestCase]
    public void ColorBackground_WithHexColorCode_ReturnsColoredBackground()
    {
        string result = Colors.ColorBackground("Hello, World!", "#FF0000");

        _ = result.ConfirmEqual(
            Log.IsHeadless
                ? "\x1b[48;2;255;0;0mHello, World!\x1b[0m"
                : "[bgcolor=#ff0000ff]Hello, World![/bgcolor]"
        );
    }

    [TestCase("#Invalid")]
    [TestCase("#1234567")]
    public void ColorBackGround_WithInvalidColorCode_ThrowsException(
        string invalidColor
    )
    {
        const string text = "Hello, World!";

        _ = Confirm.Throws<ArgumentOutOfRangeException>(
            () => Colors.ColorText(text, invalidColor)
        );
    }

    [TestCase]
    public void BackgroundToTerminal_WithColor_ReturnsColoredText()
    {
        string result = Colors.BackgroundToTerminal("Hello, World!", new Godot.Color(1, 0, 0));

        _ = result.ConfirmEqual("\x1b[48;2;255;0;0mHello, World!\x1b[0m");
    }

    [TestCase]
    public void BackgroundToGodot_WithColor_ReturnsColoredText()
    {
        string result = Colors.BackgroundToGodot("Hello, World!", new Godot.Color(1, 0, 0));

        _ = result.ConfirmEqual("[bgcolor=#ff0000ff]Hello, World![/bgcolor]");
    }
}
