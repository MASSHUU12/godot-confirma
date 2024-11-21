using System;
using System.Linq;
using System.Text.RegularExpressions;
using Confirma.Enums;
using Godot;

namespace Confirma.Helpers;

public static class TextFormatHelper
{
    private static readonly string _terminalReset = Colors.TerminalReset;

    public static int GetTagsCompactedSize(string text)
    {
        MatchCollection matches = Regex.Matches(text, @"\[([A-Za-z0-9\/=#]*\])");

        Font? font = Log.RichOutput?.GetThemeDefaultFont();
        int fontSize = Log.RichOutput?.GetThemeDefaultFontSize() ?? 16;

        if (font is null)
        {
            return 0;
        }

        return matches.Sum((Match match) => {
            string strMatch= match.Value;

            return (int)font.GetStringSize(strMatch, fontSize: fontSize).X;
        });
    }

    /// <remarks><c>EFormatType.fill</c> or <c>EFormatType.center</c> must be called before anything else (color or format) to work properly</remarks>
    public static string FormatText<T>(T text, EFormatType type, bool addReset = true)
    where T : IConvertible
    {
        return Log.IsHeadless
        ? ToTerminal(text, type, addReset)
        : ToGodot(text, type);
    }

    /// <remarks><c>EFormatType.fill</c> or <c>EFormatType.center</c> must be called before anything else (color or format) to work properly</remarks>
    public static string FormatText<T>(T text, string type, bool addReset = true)
    where T : IConvertible
    {
        return type.ToLower() switch
        {
            "bold" => FormatText(text, EFormatType.bold, addReset),
            "italic" => FormatText(text, EFormatType.italic, addReset),
            "strikethrough" => FormatText(text, EFormatType.strikethrough, addReset),
            "underline" => FormatText(text, EFormatType.underline, addReset),
            "fill" => FormatText(text, EFormatType.fill, addReset),
            "center" => FormatText(text, EFormatType.center, addReset),
            "b" => FormatText(text, EFormatType.bold, addReset),
            "i" => FormatText(text, EFormatType.italic, addReset),
            "s" => FormatText(text, EFormatType.strikethrough, addReset),
            "u" => FormatText(text, EFormatType.underline, addReset),
            "f" => FormatText(text, EFormatType.fill, addReset),
            "c" => FormatText(text, EFormatType.center, addReset),
            _ => $"{text}"
        };
    }

    /// <remarks><c>EFormatType.fill</c> or <c>EFormatType.center</c> must be called before anything else (color or format) to work properly</remarks>
    public static string ToGodot<T>(T text, EFormatType type)
    where T : IConvertible
    {
        return type switch
        {
            EFormatType.bold => $"[b]{text}[/b]",
            EFormatType.italic => $"[i]{text}[/i]",
            EFormatType.underline => $"[u]{text}[/u]",
            EFormatType.strikethrough => $"[s]{text}[/s]",
            EFormatType.fill => FillToGodot(text),
            EFormatType.center => $"[center]{text}[/center]",
            _ => $"{text}"
        };
    }

    /// <remarks><c>EFormatType.fill</c> or <c>EFormatType.center</c> must be called before anything else (color or format) to work properly</remarks>
    public static string ToTerminal<T>(T text, EFormatType type, bool addReset = true)
    where T : IConvertible
    {
        string reset = addReset ? _terminalReset : string.Empty;

        return type switch
        {
            EFormatType.bold => $"\x1b[1m{text}{reset}",
            EFormatType.italic => $"\x1b[3m{text}{reset}",
            EFormatType.underline => $"\x1b[4m{text}{reset}",
            EFormatType.strikethrough => $"\x1b[9m{text}{reset}",
            EFormatType.fill => FillToTerminal(text),
            EFormatType.center => Center(text),
            _ => $"{text}"
        };
    }

    public static string Center<T>(T text)
    where T : IConvertible
    {
        int windowWidth = Console.WindowWidth is not 0
        ? Console.WindowWidth
        : 80;

        windowWidth /= 2;
        string strText= text.ToString() ?? string.Empty;
        windowWidth -= strText.Length/2;

        if (windowWidth <= 0) { return strText; }

        return  FillToTerminal(new string (' ', windowWidth) + strText);
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

        return strText + new string (' ', windowWidth);
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
        int tagsSpace = GetTagsCompactedSize(t);
        int numSpaces = (windowWidth - currentWidth + tagsSpace) / spaceWidth;

        string paddedText = t + new string('\u00A0', numSpaces);
        currentWidth = (int)font.GetStringSize(paddedText, fontSize: fontSize).X - tagsSpace;

        // Adjust by adding or removing spaces as needed
        while (currentWidth < windowWidth)
        {
            paddedText += "\u00A0";
            currentWidth = (int)font.GetStringSize(paddedText, fontSize: fontSize).X - tagsSpace;
        }
        while (currentWidth > windowWidth && numSpaces > 0)
        {
            paddedText = paddedText.Remove(paddedText.Length - 1);
            currentWidth = (int)font.GetStringSize(paddedText, fontSize: fontSize).X - tagsSpace;
        }

        return paddedText;
    }
    #endregion Fill

    #region Link
    public static string Link<T>(T text, string url)
    where T : IConvertible
    {
        return Log.IsHeadless
        ? LinkToTerminal(text, url)
        : LinkToGodot (text, url);
    }

    public static string LinkToGodot<T>(T text, string url)
    where T : IConvertible
    {
        return $"[url={url}]{text}[/url]";
    }

    public static string LinkToTerminal<T>(T text, string url)
    where T : IConvertible
    {
        return $"\x1b]8;;{url}\x1b\\{text}\x1b]8;;\x1b\\";
    }
    #endregion Link
}
