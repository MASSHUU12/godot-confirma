using System;
using Confirma.Enums;
using Godot;

namespace Confirma.Helpers;

public static class TextFormatHelper
{
    public static string RemoveGodotTags (string FormattedText)
    {
        return FormattedText.Replace(@"\[[A-Za-z=]+\]", "");
    }

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

    #region Fill
    public static string Fill<T>(T text, int width = 0)
    where T : IConvertible
    {
        return Log.IsHeadless
            ? FillToTerminal(text, width)
            : FillToGodot (text, width);
    }

    public static string FillToTerminal<T>(T text, int width = 0)
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

        public static string FillToGodot<T>(T text, int width = 0)
        where T : IConvertible
    {
        string t = text?.ToString() ?? string.Empty;

        Font? font = Log.RichOutput?.GetThemeDefaultFont();
        int fontSize = Log.RichOutput?.GetThemeDefaultFontSize() ?? 16;

        if (font is null)
        {
            return t;
        }

        int windowWidth = (int)(Log.RichOutput?.GetRect().Size.X ?? width);
        int currentWidth = (int)font.GetStringSize(t, fontSize: fontSize).X;

        if (currentWidth >= windowWidth)
        {
            return t;
        }

        int spaceWidth = (int)font.GetStringSize("\u00A0", fontSize: fontSize).X;
        int numSpaces = (windowWidth - currentWidth) / spaceWidth;

        string paddedText = t + new string('\u00A0', numSpaces);
        currentWidth = (int)font.GetStringSize(paddedText, fontSize: fontSize).X;

        // Adjust by adding or removing spaces as needed
        while (currentWidth < windowWidth)
        {
            paddedText += "\u00A0";
            currentWidth = (int)font.GetStringSize(paddedText, fontSize: fontSize).X;
        }
        while (currentWidth > windowWidth && numSpaces > 0)
        {
            paddedText = paddedText.Remove(paddedText.Length - 1);
            currentWidth = (int)font.GetStringSize(paddedText, fontSize: fontSize).X;
        }

        return paddedText;
    }
    #endregion

    #region Link
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
        return $"[url={url}]{text}[/url]";
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
