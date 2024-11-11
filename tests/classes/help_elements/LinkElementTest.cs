using Confirma.Attributes;
using Confirma.Classes.HelpElements;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
[Category("FileElement")]
public class LinkElementTest
{
    [TestCase]
    public void GetText_ReturnsFormatedText()
    {
        LinkElement element = new ()
        {
            Url = "https://www.youtube.com/watch?v=gvYfRiJQIX8",
            Text = "link"
        };

        string result = element.GetText();
        result.ConfirmEqual(
            Log.IsHeadless
            ? "\u001b[4m\u001b[38;2;42;123;222m\u001b]8;;https://www.youtube.com/watch?v=gvYfRiJQIX8\u001b\\link\u001b]8;;\u001b\\\x1b[0m"
            : "[color=#2a7bdeff][u][url=https://www.youtube.com/watch?v=gvYfRiJQIX8]link[/url][/u][/color]"
        );
    }

    [TestCase]
    public void SetFormat_WhenSetsCorrectFormat()
    {
        LinkElement element = new ();

        element.SetFormat();

        element.FormatOverride.ConfirmCount(1);
        element.FormatOverride.ConfirmContains("u");
        element.Color.ConfirmEqual("#2a7bde");
    }
}
