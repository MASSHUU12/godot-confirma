using System;
using Godot;

namespace Confirma.Helpers;

public static class Colors
{
    public static readonly string Success = "#8eef97";
    public static readonly string Warning = "#ffdd65";
    public static readonly string Error = "#ff786b";
    public static readonly string Class = "#218a72";
    public static readonly string CSharp = "#9b4993";
    public static readonly string Gdscript = "#478cbf";
    public static readonly string TerminalReset = "\x1b[0m";

    public static string ColorText<T>(T text, string color)
    where T : IConvertible
    {
        // Note: GD.PrintRich does not support hex color codes
        // this is why we have to use different methods for terminal and Godot
        return Log.IsHeadless
            ? ToTerminal(text, new Color(color))
            : ToGodot(text, new Color(color));
    }

    public static string ColorText<T>(T text, Color color)
    where T : IConvertible
    {
        return Log.IsHeadless ? ToTerminal(text, color) : ToGodot(text, color);
    }

    public static string ToTerminal<T>(T text, Color color)
    where T : IConvertible
    {
        return $"\x1b[38;2;{color.R * 0xFF};{color.G * 0xFF};{color.B * 0xFF}m{text}{TerminalReset}";
    }

    public static string ToGodot<T>(T text, Color color)
    where T : IConvertible
    {
        return $"[color=#{color.ToHtml()}]{text}[/color]";
    }
}
