using System.Text.Json.Serialization;
using Confirma.Helpers;

namespace Confirma.Classes;

public abstract class TextElement
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [JsonPropertyName("bg_color")]
    public string? BgColor { get; set; }

    [JsonPropertyName("format")]
    public string[]? FormatOverride { get; set; }

    //I didn't had any idea how to name this method not to be misleading
    public virtual string Format(string text)
    {
        if (FormatOverride is null)
        {
            return text;
        }

        foreach(string a in FormatOverride)
        {
            text = TextFormatHelper.FormatText(text, a);
        }

        return text;
    }

    public virtual string GetText()
    {
        string text = Colors.Color(Text, Color, BgColor);

        return Format(text);
    }
}
