using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class StringExtensionsTest
{
    #region EscapeInvisibleCharacters
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
    #endregion EscapeInvisibleCharacters

    #region LevenshteinDistance
    [TestCase]
    public void LevenshteinDistance_EmptyStrings()
    {
        _ = "".LevenshteinDistance("").ConfirmIsZero();
    }

    [TestCase]
    public void LevenshteinDistance_CaseSensitivity()
    {
        _ = "Test".LevenshteinDistance("test").ConfirmEqual(1);
    }

    [TestCase("a", "", 1)]
    [TestCase("", "bb", 2)]
    [TestCase("kitten", "sitten", 1)]
    [TestCase("kitten", "kitten", 0)]
    [TestCase("kitten", "monkey", 5)]
    [TestCase("kitten", "sitting", 3)]
    public void LevenshteinDistance_ReturnsCorrectDistance(
        string a,
        string b,
        int expected
    )
    {
        _ = a.LevenshteinDistance(b).ConfirmEqual(expected);
    }
    #endregion LevenshteinDistance

    #region JaroDistance
    [TestCase("", "b", 0d)]
    [TestCase("a", "", 0d)]
    [TestCase("abc", "xyz", 0d)]
    [TestCase("example", "example", 1d)]
    [TestCase("MARTHA", "MARHTA", 0.944444444444445)]
    [TestCase("DIXON", "DICKSONX", 0.766666666666667)]
    [TestCase("JELLYFISH", "SMELLYFISH", 0.896296296296296)]
    public void JaroDistance_ReturnsCorrectDistance(
        string a,
        string b,
        double distance
    )
    {
        _ = a.JaroDistance(b).ConfirmCloseTo(distance, 0.001);
    }
    #endregion JaroDistance

    #region JaroWinklerSimilarity
    [TestCase("", "b", 0d)]
    [TestCase("a", "", 0d)]
    [TestCase("abc", "xyz", 0d)]
    [TestCase("example", "example", 1d)]
    [TestCase("MARTHA", "MARHTA", 0.961111092567444)]
    [TestCase("DIXON", "DICKSONX", 0.813333320617676)]
    [TestCase("JELLYFISH", "SMELLYFISH", 0.896296322345734)]
    public void JaroWinklerSimilarity_ReturnsCorrectDistance(
        string a,
        string b,
        double distance
    )
    {
        _ = a.JaroWinklerSimilarity(b).ConfirmCloseTo(distance, 0.001);
    }

    [TestCase]
    public void JaroWinklerSimilarity_ScalingFactorOutOfRange_ThrowsError()
    {
        Action action = static () => "Lorem".JaroWinklerSimilarity("Ipsum", 30);
        _ = action.ConfirmThrowsWMessage<ArgumentOutOfRangeException>(
            "The scaling factor must be between 0 and 0.25. (Parameter 'p')"
        );
    }
    #endregion JaroWinklerSimilarity
}
