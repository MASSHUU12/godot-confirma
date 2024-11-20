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

    [TestCase]
    public void GetText_AllExceptCenterAndFill_ReturnsFormattedText()
    {
        TextElement element = new ()
        {
            FormatOverride = {"bold","italic","strikethrough","underline"},
            Color = "#001122",
            BgColor = "#221133",
            Text="hi"
        };

        string result = element.GetText();

        result.ConfirmEqual(Log.IsHeadless

        ? "\u001b[4m\u001b[9m\u001b[3m\u001b[1m\u001b[48;2;34;17;51m\u001b[38;2;0;17;34mhi\u001b[0m"
        : "[bgcolor=#221133ff][color=#001122ff][u][s][i][b]hi[/b][/i][/s][/u][/color][/bgcolor]"
        );
    }

    [TestCase]
    public void GetText_Fill_ReturnsFormattedText()
    {
        TextElement element = new ()
        {
            FormatOverride = {"bold","italic","strikethrough","underline", "fill"},
            Color = "#001122",
            BgColor = "#221133",
            Text="hi"
        };

        string result = element.GetText();
        result.ConfirmEqual(Log.IsHeadless
        ? $"\u001b[4m\u001b[9m\u001b[3m\u001b[1m\u001b[48;2;34;17;51m\u001b[38;2;0;17;34mhi{new string(' ',System.Console.WindowWidth-2)}\u001b[0m"
        : $"[bgcolor=#221133ff][color=#001122ff][u][s][i][b]hi[/b][/i][/s][/u]{new string('\u00A0',382)}[/color][/bgcolor]"
        );
    }

        [TestCase]
    public void GetText_Center_ReturnsFormattedText()
    {
        TextElement element = new ()
        {
            FormatOverride = {"bold","italic","strikethrough","underline", "center"},
            Color = "#001122",
            BgColor = "#221133",
            Text="hi"
        };

        string result = element.GetText();
        result.ConfirmEqual(Log.IsHeadless
        ? $"\u001b[4m\u001b[9m\u001b[3m\u001b[1m\u001b[48;2;34;17;51m\u001b[38;2;0;17;34m{new string(' ',System.Console.WindowWidth/2-1)}hi{new string(' ',System.Console.WindowWidth/2-1)}\u001b[0m"
        : $"[bgcolor=#221133ff][color=#001122ff][center][u][s][i][b]hi[/b][/i][/s][/u][/center][/color][/bgcolor]"
        );
    }
}
