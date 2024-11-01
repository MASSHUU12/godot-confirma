using System.Collections.Generic;
using System.Text.Json.Serialization;
using Confirma.Deserialization.Json;
using Confirma.Helpers;

namespace Confirma.Classes;

[JsonConverter(typeof(FileElementConverter))]
public abstract class FileElement
{
    public string Text { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string BgColor { get; set; } = string.Empty;
    public List<string> FormatOverride { get; set; } = new();

    //I didn't had any idea how to name this method not to be misleading
    public virtual string Format(string text)
    {
        if (FormatOverride is null)
        {
            return text;
        }

        foreach(string? a in FormatOverride)
        {
            if (a is null)
            {
                continue;
            }

            text = TextFormatHelper.FormatText(text, a);
        }

        return text;
    }

    public virtual string GetText()
    {
        string? color, bgColor = null;

        if (Color?.Length == 0) { color = null; }
        else { color = Color; }

        if (BgColor?.Length == 0) { bgColor = null; }
        else { bgColor=BgColor; }

        string text = Colors.Color(Text, color, bgColor);

        return Format(text);
    }
}
