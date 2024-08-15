using System;
using Godot;

namespace Confirma.Helpers;

public static class Log
{
    public static RichTextLabel? RichOutput { get; set; }
    public static bool IsHeadless { get; set; } = true;

    public static void Print<T>(T message) where T : IConvertible
    {
        // Note: GD.PrintRich does not support hex color codes
        if (IsHeadless || RichOutput is null)
        {
            GD.PrintRaw(message);
        }
        else
        {
            _ = RichOutput!.CallDeferred("append_text", message.ToString()!);
        }
    }

    public static void PrintLine<T>(T message) where T : IConvertible
    {
        Print($"{message}\n");
    }

    public static void PrintLine()
    {
        Print("\n");
    }

    public static void PrintError<T>(T message) where T : IConvertible
    {
        Print(Colors.ColorText(message, Colors.Error));
    }

    public static void PrintSuccess<T>(T message) where T : IConvertible
    {
        Print(Colors.ColorText(message, Colors.Success));
    }

    public static void PrintWarning<T>(T message) where T : IConvertible
    {
        Print(Colors.ColorText(message, Colors.Warning));
    }
}
