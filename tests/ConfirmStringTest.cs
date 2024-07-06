using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmStringTest
{
    #region ConfirmEmpty
    [TestCase]
    public static void ConfirmEmpty_WhenEmpty()
    {
        const string? empty = null;

        "".ConfirmEmpty();
        empty.ConfirmEmpty();
    }

    [TestCase]
    public static void ConfirmEmpty_WhenNotEmpty()
    {
        Action action = () => "not empty".ConfirmEmpty();

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmNotEmpty
    [TestCase]
    public static void ConfirmNotEmpty_WhenNotEmpty()
    {
        "not empty".ConfirmNotEmpty();
    }

    [TestCase]
    public static void ConfirmNotEmpty_WhenEmpty()
    {
        Action action = () => "".ConfirmNotEmpty();

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmContains
    [TestCase]
    public static void ConfirmContains_WhenContains()
    {
        "contains".ConfirmContains("tai");
    }

    [TestCase]
    public static void ConfirmContains_WhenNotContains()
    {
        Action action = () => "not contains".ConfirmContains("xxx");

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmNotContains
    [TestCase]
    public static void ConfirmNotContains_WhenNotContains()
    {
        "not contains".ConfirmNotContains("xxx");
    }

    [TestCase]
    public static void ConfirmNotContains_WhenContains()
    {
        Action action = () => "contains".ConfirmNotContains("tai");

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmStartsWith
    [TestCase]
    public static void ConfirmStartsWith_WhenStartsWith()
    {
        "starts with".ConfirmStartsWith("sta");
    }

    [TestCase]
    public static void ConfirmStartsWith_WhenNotStartsWith()
    {
        Action action = () => "not starts with".ConfirmStartsWith("xxx");

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmNotStartsWith
    [TestCase]
    public static void ConfirmNotStartsWith_WhenNotStartsWith()
    {
        "not starts with".ConfirmNotStartsWith("xxx");
    }

    [TestCase]
    public static void ConfirmNotStartsWith_WhenStartsWith()
    {
        Action action = () => "starts with".ConfirmNotStartsWith("sta");

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmEndsWith
    [TestCase]
    public static void ConfirmEndsWith_WhenEndsWith()
    {
        "ends with".ConfirmEndsWith("ith");
    }

    [TestCase]
    public static void ConfirmEndsWith_WhenNotEndsWith()
    {
        Action action = () => "not ends with".ConfirmEndsWith("xxx");

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmNotEndsWith
    [TestCase]
    public static void ConfirmNotEndsWith_WhenNotEndsWith()
    {
        "not ends with".ConfirmNotEndsWith("xxx");
    }

    [TestCase]
    public static void ConfirmNotEndsWith_WhenEndsWith()
    {
        Action action = () => "ends with".ConfirmNotEndsWith("ith");

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmHasLength
    [TestCase("Lorem ipsum", 11)]
    [TestCase("Lorem ipsum\n", 12)]
    public static void ConfirmHasLength_WhenHasLength(string value, int length)
    {
        value.ConfirmHasLength(length);
    }

    [TestCase("Lorem ipsum", 12)]
    [TestCase("Lorem ipsum\n", 11)]
    public static void ConfirmHasLength_WhenNotHasLength(string value, int length)
    {
        Action action = () => value.ConfirmHasLength(length);

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmNotHasLength
    [TestCase("Lorem ipsum", 12)]
    [TestCase("Lorem ipsum\n", 11)]
    public static void ConfirmNotHasLength_WhenNotHasLength(string value, int length)
    {
        value.ConfirmNotHasLength(length);
    }

    [TestCase("Lorem ipsum", 11)]
    [TestCase("Lorem ipsum\n", 12)]
    public static void ConfirmNotHasLength_WhenHasLength(string value, int length)
    {
        Action action = () => value.ConfirmNotHasLength(length);

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmEqualsCaseInsensitive
    [TestCase("Lorem ipsum", "lorem ipsum")]
    [TestCase("Lorem ipsum", "LOREM IPSUM")]
    [TestCase("Lorem ipsum", "lOrEm IpSuM")]
    public static void ConfirmEqualsCaseInsensitive_WhenEquals(string value, string expected)
    {
        value.ConfirmEqualsCaseInsensitive(expected);
    }

    [TestCase("Lorem ipsum", "lorem")]
    [TestCase("Lorem ipsum", "ipsum")]
    [TestCase("Lorem ipsum", "lorem ipsum ")]
    public static void ConfirmEqualsCaseInsensitive_WhenNotEquals(string value, string expected)
    {
        Action action = () => value.ConfirmEqualsCaseInsensitive(expected);

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmNotEqualsCaseInsensitive
    [TestCase("Lorem ipsum", "lorem")]
    [TestCase("Lorem ipsum", "ipsum")]
    [TestCase("Lorem ipsum", "lorem ipsum ")]
    public static void ConfirmNotEqualsCaseInsensitive_WhenNotEquals(string value, string expected)
    {
        value.ConfirmNotEqualsCaseInsensitive(expected);
    }

    [TestCase("Lorem ipsum", "lorem ipsum")]
    [TestCase("Lorem ipsum", "LOREM IPSUM")]
    [TestCase("Lorem ipsum", "lOrEm IpSuM")]
    public static void ConfirmNotEqualsCaseInsensitive_WhenEquals(string value, string expected)
    {
        Action action = () => value.ConfirmNotEqualsCaseInsensitive(expected);

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmMatchesPattern
    [TestCase("Lorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})")]
    public static void ConfirmMatchesPattern_WhenPatternMatches(string value, string pattern)
    {
        value.ConfirmMatchesPattern(pattern);
    }

    [TestCase("Dorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})")]
    public static void ConfirmMatchesPattern_WhenPatternNotMatches(string value, string pattern)
    {
        Action action = () => value.ConfirmMatchesPattern(pattern);

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion

    #region ConfirmDoesNotMatchPattern
    [TestCase("Dorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})")]
    public static void ConfirmDoesNotMatchPattern_WhenPatternNotMatches(string value, string pattern)
    {
        value.ConfirmDoesNotMatchPattern(pattern);
    }

    [TestCase("Lorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})")]
    public static void ConfirmDoesNotMatchPattern_WhenPatternMatches(string value, string pattern)
    {
        Action action = () => value.ConfirmDoesNotMatchPattern(pattern);

        action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion
}
