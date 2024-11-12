using Confirma.Attributes;
using Confirma.Classes.HelpElements;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Category("FileElement")]
public class TextElementTest
{
    [TestCase]
    public void Format_EmptyFormatOverride_ReturnsClearText()
    {
        TextElement element = new ();

        string result = element.Format("Hello World!");

        result.ConfirmEqual("Hello World!");
    }

    [TestCase("f")]
    [TestCase("c")]
    [TestCase("fill")]
    [TestCase("center")]
    public void Format_SkipFillAndCenterFormat_ReturnsClearText(string format)
    {
        TextElement element = new ()
        {
            FormatOverride = {format}
        };

        string result = element.Format("Hello World!");

        result.ConfirmEqual("Hello World!");
    }

    [TestCase]
    public void Format_EveryShortFormat_ReturnsFormattedText()
    {
        TextElement element = new ()
        {
            FormatOverride = {"b","i","s","u","c","f"}
        };

        string result = element.Format("Hello World!");

        result.ConfirmEqual(Log.IsHeadless
        ? "\u001b[4m\u001b[9m\u001b[3m\u001b[1mHello World!"
        : "[u][s][i][b]Hello World![/b][/i][/s][/u]"
        );
    }

    [TestCase]
    public void Format_EveryLongFormat_ReturnsFormattedText()
    {
        TextElement element = new ()
        {
            FormatOverride = {"bold","italic","strikethrough","underline","center","fill"}
        };

        string result = element.Format("Hello World!");

        result.ConfirmEqual(Log.IsHeadless
        ? "\u001b[4m\u001b[9m\u001b[3m\u001b[1mHello World!"
        : "[u][s][i][b]Hello World![/b][/i][/s][/u]"
        );
    }
}
