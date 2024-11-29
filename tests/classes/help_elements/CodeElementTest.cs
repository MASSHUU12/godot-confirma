using System;
using Confirma.Attributes;
using Confirma.Classes.HelpElements;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Category("FileElement")]
public class CodeElementTest
{
    [TestCase]
    public void GetText_ReturnsFormattedText()
    {
        CodeElement element = new()
        {
            Lines = new()
            {
                "Line1",
                "Line2",
                "Line3",
            }
        };

        string result = element.GetText();
        result.ConfirmEqual(
            Log.IsHeadless
            ? $"\u001b[3m\u001b[48;2;13;17;23m\u001b[38;2;201;209;217mLine1{new string(' ', Console.WindowWidth-5)}" +
              $"\nLine2{new string(' ', Console.WindowWidth-5)}" +
              $"\nLine3{new string(' ', Console.WindowWidth-5)}\x1b[0m"

            : $"[bgcolor=#0d1117ff][color=#c9d1d9ff][i]Line1{new string('\u00A0',374)}\n" +
            $"Line2{new string('\u00A0',374)}\n" +
            $"Line3{new string('\u00A0',374)}" +
            "[/i][/color][/bgcolor]"
        );
    }

    [TestCase]
    public void SetFormat_WhenSetsCorrectFormat()
    {
        CodeElement element = new();

        element.SetFormat();

        element.FormatOverride.ConfirmCount(1);
        element.FormatOverride.ConfirmContains("i");
        element.Color.ConfirmEqual("#c9d1d9");
        element.BgColor.ConfirmEqual("#0d1117");
    }
}
