using System;
using Confirma.Attributes;
using Confirma.Classes;
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

        _ = "".ConfirmEmpty();
        _ = empty.ConfirmEmpty();
    }

    [TestCase]
    public static void ConfirmEmpty_WhenNotEmpty()
    {
        Action action = static () => "not empty".ConfirmEmpty();

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmEmpty

    #region ConfirmNotEmpty
    [TestCase]
    public static void ConfirmNotEmpty_WhenNotEmpty()
    {
        _ = "not empty".ConfirmNotEmpty();
    }

    [TestCase]
    public static void ConfirmNotEmpty_WhenEmpty()
    {
        Action action = static () => "".ConfirmNotEmpty();

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotEmpty

    #region ConfirmContains
    [TestCase]
    public static void ConfirmContains_WhenContains()
    {
        _ = "contains".ConfirmContains("tai");
    }

    [TestCase]
    public static void ConfirmContains_WhenNotContains()
    {
        Action action = static () => "not contains".ConfirmContains("xxx");

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmContains

    #region ConfirmNotContains
    [TestCase]
    public static void ConfirmNotContains_WhenNotContains()
    {
        _ = "not contains".ConfirmNotContains("xxx");
    }

    [TestCase]
    public static void ConfirmNotContains_WhenContains()
    {
        Action action = static () => "contains".ConfirmNotContains("tai");

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotContains

    #region ConfirmStartsWith
    [TestCase]
    public static void ConfirmStartsWith_WhenStartsWith()
    {
        _ = "starts with".ConfirmStartsWith("sta");
    }

    [TestCase]
    public static void ConfirmStartsWith_WhenNotStartsWith()
    {
        Action action = static () => "not starts with".ConfirmStartsWith("xxx");

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmStartsWith

    #region ConfirmNotStartsWith
    [TestCase]
    public static void ConfirmNotStartsWith_WhenNotStartsWith()
    {
        _ = "not starts with".ConfirmNotStartsWith("xxx");
    }

    [TestCase]
    public static void ConfirmNotStartsWith_WhenStartsWith()
    {
        Action action = static () => "starts with".ConfirmNotStartsWith("sta");

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotStartsWith

    #region ConfirmEndsWith
    [TestCase]
    public static void ConfirmEndsWith_WhenEndsWith()
    {
        _ = "ends with".ConfirmEndsWith("ith");
    }

    [TestCase]
    public static void ConfirmEndsWith_WhenNotEndsWith()
    {
        Action action = static () => "not ends with".ConfirmEndsWith("xxx");

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmEndsWith

    #region ConfirmNotEndsWith
    [TestCase]
    public static void ConfirmNotEndsWith_WhenNotEndsWith()
    {
        _ = "not ends with".ConfirmNotEndsWith("xxx");
    }

    [TestCase]
    public static void ConfirmNotEndsWith_WhenEndsWith()
    {
        Action action = static () => "ends with".ConfirmNotEndsWith("ith");

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotEndsWith

    #region ConfirmHasLength
    [TestCase("Lorem ipsum", 11)]
    [TestCase("Lorem ipsum\n", 12)]
    public static void ConfirmHasLength_WhenHasLength(string value, int length)
    {
        _ = value.ConfirmHasLength(length);
    }

    [TestCase("Lorem ipsum", 12)]
    [TestCase("Lorem ipsum\n", 11)]
    public static void ConfirmHasLength_WhenNotHasLength(string value, int length)
    {
        Action action = () => value.ConfirmHasLength(length);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmHasLength

    #region ConfirmNotHasLength
    [TestCase("Lorem ipsum", 12)]
    [TestCase("Lorem ipsum\n", 11)]
    public static void ConfirmNotHasLength_WhenNotHasLength(string value, int length)
    {
        _ = value.ConfirmNotHasLength(length);
    }

    [TestCase("Lorem ipsum", 11)]
    [TestCase("Lorem ipsum\n", 12)]
    public static void ConfirmNotHasLength_WhenHasLength(string value, int length)
    {
        Action action = () => value.ConfirmNotHasLength(length);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotHasLength

    #region ConfirmEqualsCaseInsensitive
    [TestCase("Lorem ipsum", "lorem ipsum")]
    [TestCase("Lorem ipsum", "LOREM IPSUM")]
    [TestCase("Lorem ipsum", "lOrEm IpSuM")]
    public static void ConfirmEqualsCaseInsensitive_WhenEquals(
        string value,
        string expected
    )
    {
        _ = value.ConfirmEqualsCaseInsensitive(expected);
    }

    [TestCase("Lorem ipsum", "lorem")]
    [TestCase("Lorem ipsum", "ipsum")]
    [TestCase("Lorem ipsum", "lorem ipsum ")]
    public static void ConfirmEqualsCaseInsensitive_WhenNotEquals(
        string value,
        string expected
    )
    {
        Action action = () => value.ConfirmEqualsCaseInsensitive(expected);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmEqualsCaseInsensitive

    #region ConfirmNotEqualsCaseInsensitive
    [TestCase("Lorem ipsum", "lorem")]
    [TestCase("Lorem ipsum", "ipsum")]
    [TestCase("Lorem ipsum", "lorem ipsum ")]
    public static void ConfirmNotEqualsCaseInsensitive_WhenNotEquals(
        string value,
        string expected
    )
    {
        _ = value.ConfirmNotEqualsCaseInsensitive(expected);
    }

    [TestCase("Lorem ipsum", "lorem ipsum")]
    [TestCase("Lorem ipsum", "LOREM IPSUM")]
    [TestCase("Lorem ipsum", "lOrEm IpSuM")]
    public static void ConfirmNotEqualsCaseInsensitive_WhenEquals(
        string value,
        string expected
    )
    {
        Action action = () => value.ConfirmNotEqualsCaseInsensitive(expected);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotEqualsCaseInsensitive

    #region ConfirmMatchesPattern
    [TestCase("Lorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})")]
    public static void ConfirmMatchesPattern_WhenPatternMatches(
        string value,
        string pattern
    )
    {
        _ = value.ConfirmMatchesPattern(pattern);
    }

    [TestCase("Dorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})")]
    public static void ConfirmMatchesPattern_WhenPatternNotMatches(
        string value,
        string pattern
    )
    {
        Action action = () => value.ConfirmMatchesPattern(pattern);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmMatchesPattern

    #region ConfirmDoesNotMatchPattern
    [TestCase("Dorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})")]
    public static void ConfirmDoesNotMatchPattern_WhenPatternNotMatches(
        string value,
        string pattern
    )
    {
        _ = value.ConfirmDoesNotMatchPattern(pattern);
    }

    [TestCase("Lorem ipsum", @"\bL\w*\b")]
    [TestCase("123-456-789", @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})")]
    public static void ConfirmDoesNotMatchPattern_WhenPatternMatches(
        string value,
        string pattern
    )
    {
        Action action = () => value.ConfirmDoesNotMatchPattern(pattern);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmDoesNotMatchPattern

    #region ConfirmLowercase
    [TestCase]
    public static void ConfirmLowercase_ReturnsTrue_WhenStringIsLowercase()
    {
        _ = "hello".ConfirmLowercase().ConfirmTrue();
    }

    [TestCase("Hello")]
    [TestCase("heLlo")]
    [TestCase("hellO")]
    [TestCase("HELLO")]
    public static void ConfirmLowercase_WhenStringIsNotLowercase(string str)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => str.ConfirmLowercase());
    }
    #endregion ConfirmLowercase

    #region ConfirmUppercase
    [TestCase]
    public static void ConfirmUppercase_ReturnsTrue_WhenStringIsUppercase()
    {
        _ = "HELLO".ConfirmUppercase().ConfirmTrue();
    }

    [TestCase("hello")]
    [TestCase("Hello")]
    [TestCase("heLlo")]
    [TestCase("hellO")]
    public static void ConfirmUppercase_WhenStringIsNotUppercase(string str)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => str.ConfirmUppercase());
    }
    #endregion ConfirmUppercase
}
