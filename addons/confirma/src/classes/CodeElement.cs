using System.Collections.Generic;
using System.Linq;

namespace Confirma.Classes;

public class CodeElement : FileElement
{
    public List<string> Lines { get; set; } = new ();

    public override string GetText()
    {
        SetFormat();

        foreach (string line in Lines)
        {
            Text += $"{line}\n";
        }
        Text = Text.TrimEnd('\n');

        return base.GetText();
    }

    public void SetFormat()
    {
        FormatOverride = new List<string>() { "i","f" };
        BgColor = "#0d1117";
        Color = "#c9d1d9";
    }
}
