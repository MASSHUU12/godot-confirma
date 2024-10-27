using System;
using Confirma.Enums;

namespace Confirma.Helpers;

public static class TextFormatHelper
{
    public static string FormatText<T> (
        T text,
        EFormatType type
    )
    where T : IConvertible
    {
        return Log.IsHeadless
        ? ToTerminal (text, type)
        : ToGodot (text, type);

    }

    public static string ToGodot<T> (
        T text,
        EFormatType type
    )
    where T : IConvertible
    {
        return type switch
        {
            EFormatType.bold => $"[b]{text}[/b]",
            EFormatType.italic => $"[i]{text}[/i]",
            EFormatType.underline => $"[u]{text}[/u]",
            EFormatType.strikethrough => $"[s]{text}[/s]",
            _ => $"{text}"
        };
    }

    public static string ToTerminal<T> (
        T text,
        EFormatType type
    )
    where T : IConvertible
    {
        return type switch
        {
            EFormatType.bold => $"\x1b[1m{text}\x1b[0m",
            EFormatType.italic => $"\x1b[3m{text}\x1b[0m",
            EFormatType.underline => $"\x1b[4m{text}\x1b[0m",
            EFormatType.strikethrough => $"\x1b[9m{text}\x1b[0m",
            _ => $"{text}"
        };
    }
}
