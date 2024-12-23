using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Enums;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmStringTest
{
    #region ConfirmEmpty
    [TestCase]
    public void ConfirmEmpty_WhenEmpty()
    {
        const string? empty = null;

        _ = "".ConfirmEmpty();
        _ = empty.ConfirmEmpty();
    }

    [TestCase]
    public void ConfirmEmpty_WhenNotEmpty()
    {
        Action action = static () => "not empty".ConfirmEmpty();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEmpty failed: "
            + "Expected empty string, but got \"not empty\"."
        );
    }
    #endregion ConfirmEmpty

    #region ConfirmNotEmpty
    [TestCase]
    public void ConfirmNotEmpty_WhenNotEmpty()
    {
        _ = "not empty".ConfirmNotEmpty();
    }

    [TestCase]
    public void ConfirmNotEmpty_WhenEmpty()
    {
        Action action = static () => "".ConfirmNotEmpty();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEmpty failed: Expected non-empty string."
        );
    }
    #endregion ConfirmNotEmpty

    #region ConfirmContains
    [TestCase]
    public void ConfirmContains_WhenContains()
    {
        _ = "contains".ConfirmContains("tai");
    }

    [TestCase]
    public void ConfirmContains_WhenNotContains()
    {
        Action action = static () => "not contains".ConfirmContains("xxx");

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmContains failed: "
            + "Expected string to contain \"xxx\", but got \"not contains\"."
        );
    }
    #endregion ConfirmContains

    #region ConfirmNotContains
    [TestCase]
    public void ConfirmNotContains_WhenNotContains()
    {
        _ = "not contains".ConfirmNotContains("xxx");
    }

    [TestCase]
    public void ConfirmNotContains_WhenContains()
    {
        Action action = static () => "contains".ConfirmNotContains("tai");

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotContains failed: "
            + "Expected string to not contain \"tai\", but got \"contains\"."
        );
    }
    #endregion ConfirmNotContains

    #region ConfirmStartsWith
    [TestCase]
    public void ConfirmStartsWith_WhenStartsWith()
    {
        _ = "starts with".ConfirmStartsWith("sta");
    }

    [TestCase]
    public void ConfirmStartsWith_WhenNotStartsWith()
    {
        Action action = static () => "not starts with".ConfirmStartsWith("xxx");

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmStartsWith failed: Expected string "
            + "to start with \"xxx\", but got \"not starts with\"."
        );
    }
    #endregion ConfirmStartsWith

    #region ConfirmNotStartsWith
    [TestCase]
    public void ConfirmNotStartsWith_WhenNotStartsWith()
    {
        _ = "not starts with".ConfirmNotStartsWith("xxx");
    }

    [TestCase]
    public void ConfirmNotStartsWith_WhenStartsWith()
    {
        Action action = static () => "starts with".ConfirmNotStartsWith("sta");

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotStartsWith failed: Expected string "
            + "to not start with \"sta\", but got \"starts with\"."
        );
    }
    #endregion ConfirmNotStartsWith

    #region ConfirmEndsWith
    [TestCase]
    public void ConfirmEndsWith_WhenEndsWith()
    {
        _ = "ends with".ConfirmEndsWith("ith");
    }

    [TestCase]
    public void ConfirmEndsWith_WhenNotEndsWith()
    {
        Action action = static () => "not ends with".ConfirmEndsWith("xxx");

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEndsWith failed: Expected string "
            + "to end with \"xxx\", but got \"not ends with\"."
        );
    }
    #endregion ConfirmEndsWith

    #region ConfirmNotEndsWith
    [TestCase]
    public void ConfirmNotEndsWith_WhenNotEndsWith()
    {
        _ = "not ends with".ConfirmNotEndsWith("xxx");
    }

    [TestCase]
    public void ConfirmNotEndsWith_WhenEndsWith()
    {
        Action action = static () => "ends with".ConfirmNotEndsWith("ith");

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEndsWith failed: Expected string "
            + "to not end with \"ith\", but got \"ends with\"."
        );
    }
    #endregion ConfirmNotEndsWith

    #region ConfirmHasLength
    [TestCase("Lorem ipsum", 11)]
    [TestCase("Lorem ipsum\n", 12)]
    public void ConfirmHasLength_WhenHasLength(string value, int length)
    {
        _ = value.ConfirmHasLength(length);
    }

    [TestCase("Lorem ipsum", 12)]
    [TestCase("Lorem ipsum\n", 11)]
    public void ConfirmHasLength_WhenNotHasLength(string value, int length)
    {
        Action action = () => value.ConfirmHasLength(length);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmHasLength failed: Expected string "
            + $"to have length of {length}, but got {value.Length}."
        );
    }
    #endregion ConfirmHasLength

    #region ConfirmNotHasLength
    [TestCase("Lorem ipsum", 12)]
    [TestCase("Lorem ipsum\n", 11)]
    public void ConfirmNotHasLength_WhenNotHasLength(string value, int length)
    {
        _ = value.ConfirmNotHasLength(length);
    }

    [TestCase("Lorem ipsum", 11)]
    [TestCase("Lorem ipsum\n", 12)]
    public void ConfirmNotHasLength_WhenHasLength(string value, int length)
    {
        Action action = () => value.ConfirmNotHasLength(length);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotHasLength failed: Expected string "
            + $"to not have length of {length}."
        );
    }
    #endregion ConfirmNotHasLength

    #region ConfirmEqualsCaseInsensitive
    [TestCase("Lorem ipsum", "lorem ipsum")]
    [TestCase("Lorem ipsum", "LOREM IPSUM")]
    [TestCase("Lorem ipsum", "lOrEm IpSuM")]
    public void ConfirmEqualsCaseInsensitive_WhenEquals(
        string value,
        string expected
    )
    {
        _ = value.ConfirmEqualsCaseInsensitive(expected);
    }

    [TestCase("Lorem ipsum", "lorem")]
    [TestCase("Lorem ipsum", "ipsum")]
    [TestCase("Lorem ipsum", "lorem ipsum ")]
    public void ConfirmEqualsCaseInsensitive_WhenNotEquals(
        string value,
        string expected
    )
    {
        Action action = () => value.ConfirmEqualsCaseInsensitive(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEqualsCaseInsensitive failed: Expected string "
            + $"to equal \"{value}\", but got \"{expected}\"."
        );
    }
    #endregion ConfirmEqualsCaseInsensitive

    #region ConfirmNotEqualsCaseInsensitive
    [TestCase("Lorem ipsum", "lorem")]
    [TestCase("Lorem ipsum", "ipsum")]
    [TestCase("Lorem ipsum", "lorem ipsum ")]
    public void ConfirmNotEqualsCaseInsensitive_WhenNotEquals(
        string value,
        string expected
    )
    {
        _ = value.ConfirmNotEqualsCaseInsensitive(expected);
    }

    [TestCase("Lorem ipsum", "lorem ipsum")]
    [TestCase("Lorem ipsum", "LOREM IPSUM")]
    [TestCase("Lorem ipsum", "lOrEm IpSuM")]
    public void ConfirmNotEqualsCaseInsensitive_WhenEquals(
        string value,
        string expected
    )
    {
        Action action = () => value.ConfirmNotEqualsCaseInsensitive(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEqualsCaseInsensitive failed: Expected string "
            + $"to not equal \"{value}\"."
        );
    }
    #endregion ConfirmNotEqualsCaseInsensitive

    #region ConfirmMatchesPattern
    [TestCase("Lorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})")]
    public void ConfirmMatchesPattern_WhenPatternMatches(
        string value,
        string pattern
    )
    {
        _ = value.ConfirmMatchesPattern(pattern);
    }

    [TestCase("Dorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})")]
    public void ConfirmMatchesPattern_WhenPatternNotMatches(
        string value,
        string pattern
    )
    {
        Action action = () => value.ConfirmMatchesPattern(pattern);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmMatchesPattern failed: Expected string "
            + $"\"{value}\" to match pattern \"{pattern}\"."
        );
    }
    #endregion ConfirmMatchesPattern

    #region ConfirmDoesNotMatchPattern
    [TestCase("Dorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})")]
    public void ConfirmDoesNotMatchPattern_WhenPatternNotMatches(
        string value,
        string pattern
    )
    {
        _ = value.ConfirmDoesNotMatchPattern(pattern);
    }

    [TestCase("Lorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})")]
    public void ConfirmDoesNotMatchPattern_WhenPatternMatches(
        string value,
        string pattern
    )
    {
        Action action = () => value.ConfirmDoesNotMatchPattern(pattern);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmDoesNotMatchPattern failed: Expected string "
            + $"\"{value}\" to not match pattern \"{pattern}\"."
        );
    }
    #endregion ConfirmDoesNotMatchPattern

    #region ConfirmLowercase
    [TestCase]
    public void ConfirmLowercase_ReturnsTrue_WhenStringIsLowercase()
    {
        _ = "hello".ConfirmLowercase().ConfirmTrue();
    }

    [TestCase("Hello")]
    [TestCase("heLlo")]
    [TestCase("hellO")]
    [TestCase("HELLO")]
    public void ConfirmLowercase_WhenStringIsNotLowercase(string str)
    {
        Action action = () => str.ConfirmLowercase();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmLowercase failed: Expected "
            + $"\"{str}\" to be lowercase."
        );
    }
    #endregion ConfirmLowercase

    #region ConfirmUppercase
    [TestCase]
    public void ConfirmUppercase_ReturnsTrue_WhenStringIsUppercase()
    {
        _ = "HELLO".ConfirmUppercase().ConfirmTrue();
    }

    [TestCase("hello")]
    [TestCase("Hello")]
    [TestCase("heLlo")]
    [TestCase("hellO")]
    public void ConfirmUppercase_WhenStringIsNotUppercase(string str)
    {
        Action action = () => str.ConfirmUppercase();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmUppercase failed: Expected "
            + $"\"{str}\" to be uppercase."
        );
    }
    #endregion ConfirmUppercase

    #region ConfirmSimilar
    [TestCase("a", "", 0d)]
    [TestCase("", "bb", 0d)]
    [TestCase("kitten", "sitten", 0.75)]
    [TestCase("kitten", "sitting", 0.5)]
    public void ConfirmSimilar_LevenshteinDistance_WhenIsSimilar(
        string a,
        string b,
        double expected
    )
    {
        _ = a.ConfirmSimilar(b, expected, EStringSimilarityMethod.LevenshteinDistance);
    }

    [TestCase]
    public void ConfirmSimilar_LevenshteinDistance_WhenIsNotSimilar()
    {
        Action action = static () => "a".ConfirmSimilar(
            "abc",
            1d,
            EStringSimilarityMethod.LevenshteinDistance
        );

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmSimilar failed: "
            + "String \"a\" is not similar to \"abc\" with a score of 0.3333333134651184. "
            + "Expected a score of at least 1."
        );
    }

    [TestCase("", "b", 0d)]
    [TestCase("a", "", 0d)]
    [TestCase("abc", "xyz", 0d)]
    [TestCase("JELLYFISH", "SMELLYFISH", 0.896296296296296)]
    public void ConfirmSimilar_JaroDistance_WhenIsSimilar(
        string a,
        string b,
        double expected
    )
    {
        _ = a.ConfirmSimilar(b, expected, EStringSimilarityMethod.JaroDistance);
    }

    [TestCase]
    public void ConfirmSimilar_JaroDistance_WhenIsNotSimilar()
    {
        Action action = static () => "a".ConfirmSimilar(
            "abc",
            1d,
            EStringSimilarityMethod.JaroDistance
        );

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmSimilar failed: "
            + "String \"a\" is not similar to \"abc\" with a score of 0.7777777777777777. "
            + "Expected a score of at least 1."
        );
    }

    [TestCase("", "b", 0d)]
    [TestCase("a", "", 0d)]
    [TestCase("abc", "xyz", 0d)]
    [TestCase("JELLYFISH", "SMELLYFISH", 0.896296296296296)]
    public void ConfirmSimilar_JaroWinklerSimilarity_WhenIsSimilar(
        string a,
        string b,
        double expected
    )
    {
        _ = a.ConfirmSimilar(b, expected, EStringSimilarityMethod.JaroWinklerSimilarity);
    }

    [TestCase]
    public void ConfirmSimilar_JaroWinklerSimilarity_WhenIsNotSimilar()
    {
        Action action = static () => "a".ConfirmSimilar(
            "abc",
            1d,
            EStringSimilarityMethod.JaroWinklerSimilarity
        );

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmSimilar failed: "
            + "String \"a\" is not similar to \"abc\" with a score of 0.7999999999999999. "
            + "Expected a score of at least 1."
        );
    }
    #endregion ConfirmSimilar

    #region ConfirmNotSimilar
    [TestCase("a", "", 0d)]
    [TestCase("", "bb", 0d)]
    [TestCase("kitten", "sitten", 0.8333333283662796)]
    [TestCase("kitten", "sitting", 0.5714285671710968)]
    public void ConfirmNotSimilar_LevenshteinDistance_WhenIsNotSimilar(
        string a,
        string b,
        double expected
    )
    {
        _ = a.ConfirmNotSimilar(b, expected, EStringSimilarityMethod.LevenshteinDistance);
    }

    [TestCase("a", "", 0d)]
    [TestCase("", "bb", 0d)]
    [TestCase("kitten", "sitten", 0.888888888888889)]
    [TestCase("kitten", "sitting", 0.746031746031746)]
    public void ConfirmNotSimilar_JaroDistance_WhenIsNotSimilar(
        string a,
        string b,
        double expected
    )
    {
        _ = a.ConfirmNotSimilar(b, expected, EStringSimilarityMethod.JaroDistance);
    }

    [TestCase("a", "", 0d)]
    [TestCase("", "bb", 0d)]
    [TestCase("kitten", "sitten", 0.888888888888889)]
    [TestCase("kitten", "sitting", 0.746031746031746)]
    public void ConfirmNotSimilar_JaroWinklerSimilarity_WhenIsNotSimilar(
        string a,
        string b,
        double expected
    )
    {
        _ = a.ConfirmNotSimilar(b, expected, EStringSimilarityMethod.JaroWinklerSimilarity);
    }

    [TestCase]
    public void ConfirmNotSimilar_WhenIsSimilar()
    {
        Action action = static () => "a".ConfirmNotSimilar(
            "a",
            0.5,
            EStringSimilarityMethod.LevenshteinDistance
        );

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotSimilar failed: "
            + "String \"a\" is similar to \"a\" with a score of 1. "
            + "Expected a score of at most 0.5."
        );
    }
    #endregion ConfirmNotSimilar
}
