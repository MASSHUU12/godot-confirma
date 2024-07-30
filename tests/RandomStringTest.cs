using System;
using System.Linq;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using static Confirma.Extensions.RandomStringExtensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomStringTest
{
    private static readonly Random _rg = new();

    [Repeat(3)]
    [TestCase]
    public static void NextChar_Mixed_ReturnsUpperOrLowerChar()
    {
        char result = _rg.NextChar(StringType.MixedLetter);

        _ = Confirm.IsTrue(
            (char.IsUpper(result) || char.IsLower(result))
            && !char.IsDigit(result)
        );
    }

    [TestCase("abc")]
    [TestCase("123")]
    [TestCase("A$B2c_")]
    public static void NextChar_AllowedChars_ReturnsCharFromAllowedChars(string allowedChars)
    {
        _ = allowedChars.ConfirmContains(_rg.NextChar(allowedChars));
    }

    [Repeat(3)]
    [TestCase]
    public static void NextLowerChar_GeneratesLowercaseLetter()
    {
        char result = _rg.NextLowerChar();

        _ = Confirm.IsTrue(char.IsLower(result));
        _ = Confirm.IsTrue(result is >= 'a' and <= 'z');
    }

    [Repeat(3)]
    [TestCase]
    public static void NextUpperChar_GeneratesUppercaseLetter()
    {
        char result = _rg.NextUpperChar();

        _ = Confirm.IsTrue(char.IsUpper(result));
        _ = Confirm.IsTrue(result is >= 'A' and <= 'Z');
    }

    [Repeat(3)]
    [TestCase]
    public static void NextDigitChar_GeneratesDigitLetter()
    {
        _ = Confirm.IsTrue(char.IsDigit(_rg.NextDigitChar()));
    }

    #region NextString
    [Repeat(3)]
    [TestCase]
    public static void NextString_DefaultLength_ReturnsStringWithDefaultLength()
    {
        string result = _rg.NextString();

        _ = Confirm.IsTrue(result.Length is >= 8 and <= 12);
        _ = Confirm.IsTrue(result.All(static (c) => !char.IsDigit(c)));
    }

    [Repeat(3)]
    [TestCase]
    public static void NextString_CustomLength_ReturnsStringWithCustomLength()
    {
        const int minLength = 5;
        const int maxLength = 10;

        string result = _rg.NextString(minLength, maxLength);

        _ = Confirm.IsTrue(result.Length is >= minLength and <= maxLength);
        _ = Confirm.IsTrue(result.All(static (c) => !char.IsDigit(c)));
    }

    [TestCase]
    public static void NextString_InvalidLength()
    {
        Action action = static () => _rg.NextString(15, 5);

        _ = action.ConfirmThrows<ArgumentOutOfRangeException>();
    }

    [Repeat(3)]
    [TestCase]
    public static void NextString_UpperType_ReturnsStringWithCustomType()
    {
        string result = _rg.NextString(type: StringType.Upper);

        _ = Confirm.IsTrue(result.All(char.IsUpper));
    }

    [Repeat(3)]
    [TestCase]
    public static void NextString_LowerType_ReturnsStringWithCustomType()
    {
        string result = _rg.NextString(type: StringType.Lower);

        _ = Confirm.IsTrue(result.All(char.IsLower));
    }

    [Repeat(3)]
    [TestCase]
    public static void NextString_DigitType_ReturnsStringWithCustomType()
    {
        string result = _rg.NextString(type: StringType.Digit);

        _ = Confirm.IsTrue(result.All(char.IsDigit));
    }
    #endregion NextString
}
