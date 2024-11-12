using Confirma.Attributes;
using Confirma.Classes.HelpElements;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Category("FileElement")]
public class HeaderElementTest
{
    [TestCase (1)]
    [TestCase (2)]
    [TestCase (3)]
    [TestCase (4)]
    public void SetFormat_WhenSetsCorrectFormat(int level)
    {
        HeaderElement element = new ();

        element.SetFormat(level);

        switch(level)
        {
            case 1:
                element.FormatOverride.ToArray().ConfirmEqual(new string[]{ "b", "i", "f" });
                element.BgColor.ConfirmEqual("#edede9");
                element.Color.ConfirmEqual("#000");
                break;
            case 2:
                element.FormatOverride.ToArray().ConfirmEqual(new string[]{"i","f"});
                element.BgColor.ConfirmEqual("#a8a8a3");
                element.Color.ConfirmEqual("#000");
                break;
            case 3:
                element.FormatOverride.ToArray().ConfirmEqual(new string[]{"b","u"});
                element.Color.ConfirmEqual("#fff");
                break;
            case 4:
                element.FormatOverride.ConfirmCount(1);
                element.FormatOverride.ConfirmContains("i");
                element.Color.ConfirmEqual("#fff");
                break;
        }
    }
}
