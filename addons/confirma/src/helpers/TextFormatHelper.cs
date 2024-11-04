using System;
using Confirma.Enums;

namespace Confirma.Helpers;

public static class TextFormatHelper
{
     /// <remarks><c>EFormatType.fill</c> or <c>EFormatType.center</c> must be called before anything else (color or format) to work properly</remarks>
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

     /// <remarks><c>EFormatType.fill</c> or <c>EFormatType.center</c> must be called before anything else (color or format) to work properly</remarks>
    public static string FormatText<T> (
        T text,
        string type
    )
    where T : IConvertible
    {
        return type.ToLower() switch
        {
            "bold" => FormatText(text, EFormatType.bold),
            "italic" => FormatText(text, EFormatType.italic),
            "strikethrough" => FormatText(text, EFormatType.strikethrough),
            "underline" => FormatText(text, EFormatType.underline),
            "fill" => FormatText(text, EFormatType.fill),
            "center" => FormatText(text,EFormatType.center),
            "b" => FormatText(text, EFormatType.bold),
            "i" => FormatText(text, EFormatType.italic),
            "s" => FormatText(text, EFormatType.strikethrough),
            "u" => FormatText(text, EFormatType.underline),
            "f" => FormatText(text, EFormatType.fill),
            "c" => FormatText(text,EFormatType.center),
            _ => $"{text}"
        };
    }

    /// <remarks><c>EFormatType.fill</c> or <c>EFormatType.center</c> must be called before anything else (color or format) to work properly</remarks>
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
            EFormatType.fill => Fill(text),
            EFormatType.center => $"[center]{text}[/center]",
            _ => $"{text}"
        };
    }

    /// <remarks><c>EFormatType.fill</c> or <c>EFormatType.center</c> must be called before anything else (color or format) to work properly</remarks>
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
            EFormatType.fill => Fill(text),
            EFormatType.center => Center(text),
            _ => $"{text}"
        };
    }

    public static string Fill<T>
    (
        T text,
        int width = 0
    )
    where T : IConvertible
    {
        int windowWidth = Console.WindowWidth is not 0
        ? Console.WindowWidth
        : width is not 0
            ? width
            : 80;

        string strText= text.ToString() ?? string.Empty;
        windowWidth -= strText.Length;

        if (windowWidth <= 0) { return strText; }

        return strText + new string (' ',windowWidth);
    }

    public static string Center<T>
    (
        T text
    )
    where T : IConvertible
    {
        int windowWidth = Console.WindowWidth is not 0
        ? Console.WindowWidth
        : 80;

        windowWidth /= 2;
        string strText= text.ToString() ?? string.Empty;
        windowWidth -= strText.Length/2;

        if (windowWidth <= 0) { return strText; }

        return  Fill(new string (' ',windowWidth) + strText);
    }

    #region link
    public static string Link<T>
    (
        T text,
        string url
    )
    where T : IConvertible
    {
        return Log.IsHeadless
        ? LinkToTerminal(text, url)
        : LinkToGodot (text, url);
    }

    public static string LinkToGodot<T>
    (
        T text,
        string url
    )
    where T : IConvertible
    {
        return $"[link={url}]{text}[link]";
    }

    public static string LinkToTerminal<T>
    (
        T text,
        string url
    )
    where T : IConvertible
    {
        return $"\x1b]8;;{url}\x1b\\{text}\x1b]8;;\x1b\\";
    }
    #endregion
}
