using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class StringExtensionsTest
{
    [TestCase]
    public void EscapeInvisibleCharacters_EmptyString_ReturnsEmptyString()
    {
        _ = string.Empty.EscapeInvisibleCharacters().ConfirmEqual(string.Empty);
    }

    [TestCase]
    public void EscapeInvisibleCharacters_NoInvisibleCharacters_ReturnsOriginalString()
    {
        _ = "Hello World".EscapeInvisibleCharacters().ConfirmEqual("Hello World");
    }

    [TestCase("\0", "\\0")] // Null
    [TestCase("\a", "\\a")] // Alert
    [TestCase("\b", "\\b")] // Backspace
    [TestCase("\f", "\\f")] // Form feed
    [TestCase("\n", "\\n")] // New line
    [TestCase("\r", "\\r")] // Carriage return
    [TestCase("\t", "\\t")] // Horizontal tab
    [TestCase("\v", "\\v")] // Vertical tab
    public void EscapeInvisibleCharacters_SpecialCharacter_ReturnsEscapedCharacter(
        string character,
        string expected
    )
    {
        _ = character.EscapeInvisibleCharacters().ConfirmEqual(expected);
    }

    [TestCase]
    public void EscapeInvisibleCharacters_MultipleInvisibleCharacters_ReturnsEscapedInvisibleCharacters()
    {
        _ = "\0\a\b\f\n\r\t\v"
            .EscapeInvisibleCharacters()
            .ConfirmEqual("\\0\\a\\b\\f\\n\\r\\t\\v");
    }
}
