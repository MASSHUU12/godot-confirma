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
    private readonly static Random _rg = new();

    [Repeat(3)]
    [TestCase]
    public static void NextChar_Mixed_ReturnsUpperOrLowerChar()
    {
        var result = _rg.NextChar(StringType.MixedLetter);

        Confirm.IsTrue(
            (char.IsUpper(result) || char.IsLower(result))
            && !char.IsDigit(result)
        );
    }

    [TestCase("abc")]
    [TestCase("123")]
    [TestCase("A$B2c_")]
    public static void NextChar_AllowedChars_ReturnsCharFromAllowedChars(string allowedChars)
    {
        allowedChars.ConfirmContains(_rg.NextChar(allowedChars));
    }

    [Repeat(3)]
    [TestCase]
    public static void NextLowerChar_GeneratesLowercaseLetter()
    {
        var result = _rg.NextLowerChar();

        Confirm.IsTrue(char.IsLower(result));
        Confirm.IsTrue(result >= 'a' && result <= 'z');
    }

    [Repeat(3)]
    [TestCase]
    public static void NextUpperChar_GeneratesUppercaseLetter()
    {
        var result = _rg.NextUpperChar();

        Confirm.IsTrue(char.IsUpper(result));
        Confirm.IsTrue(result >= 'A' && result <= 'Z');
    }

    [Repeat(3)]
    [TestCase]
    public static void NextDigitChar_GeneratesDigitLetter()
    {
        Confirm.IsTrue(char.IsDigit(_rg.NextDigitChar()));
    }

    #region NextString
    [Repeat(3)]
    [TestCase]
    public static void NextString_DefaultLength_ReturnsStringWithDefaultLength()
    {
        var result = _rg.NextString();

        Confirm.IsTrue(result.Length >= 8 && result.Length <= 12);
        Confirm.IsTrue(result.All((c) => !char.IsDigit(c)));
    }

    [Repeat(3)]
    [TestCase]
    public static void NextString_CustomLength_ReturnsStringWithCustomLength()
    {
        var minLength = 5;
        var maxLength = 10;

        var result = _rg.NextString(minLength, maxLength);

        Confirm.IsTrue(result.Length >= minLength && result.Length <= maxLength);
        Confirm.IsTrue(result.All((c) => !char.IsDigit(c)));
    }

    [TestCase]
    public static void NextString_InvalidLength()
    {
        Action action = () => _rg.NextString(15, 5);

        action.ConfirmThrows<ArgumentOutOfRangeException>();
    }

    [Repeat(3)]
    [TestCase]
    public static void NextString_UpperType_ReturnsStringWithCustomType()
    {
        var result = _rg.NextString(type: StringType.Upper);

        Confirm.IsTrue(result.All(char.IsUpper));
    }

    [Repeat(3)]
    [TestCase]
    public static void NextString_LowerType_ReturnsStringWithCustomType()
    {
        var result = _rg.NextString(type: StringType.Lower);

        Confirm.IsTrue(result.All(char.IsLower));
    }

    [Repeat(3)]
    [TestCase]
    public static void NextString_DigitType_ReturnsStringWithCustomType()
    {
        var result = _rg.NextString(type: StringType.Digit);

        Confirm.IsTrue(result.All(char.IsDigit));
    }
    #endregion
}
