using System;
using System.Text.RegularExpressions;
using Confirma.Exceptions;
using Confirma.Formatters;
using static System.StringComparison;

namespace Confirma.Extensions;

public static class ConfirmStringExtensions
{
    #region ConfirmEmpty
    public static string? ConfirmEmpty(
        this string? actual,
        string? message = null
    )
    {
        if (string.IsNullOrEmpty(actual))
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected empty string, but got {1}.",
            nameof(ConfirmEmpty),
            new StringFormatter(),
            actual,
            null,
            message
        );
    }

    public static string? ConfirmNotEmpty(
        this string? actual,
        string? message = null
    )
    {
        if (!string.IsNullOrEmpty(actual))
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected non-empty string.",
            nameof(ConfirmNotEmpty),
            null,
            null,
            null,
            message
        );
    }
    #endregion ConfirmEmpty

    #region ConfirmContains
    public static string? ConfirmContains(
        this string? actual,
        string expected,
        StringComparison comparisonType = OrdinalIgnoreCase,
        string? message = null
    )
    {
        if (actual?.Contains(expected, comparisonType) == true)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to contain {1}, but got {2}.",
            nameof(ConfirmContains),
            new StringFormatter(),
            expected,
            actual,
            message
        );
    }

    public static string? ConfirmNotContains(
        this string? actual,
        string expected,
        string? message = null
    )
    {
        if (actual?.Contains(expected) == false)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to not contain {1}, but got {2}.",
            nameof(ConfirmNotContains),
            new StringFormatter(),
            expected,
            actual,
            message
        );
    }
    #endregion ConfirmContains

    #region ConfirmStartsWith
    public static string? ConfirmStartsWith(
        this string? actual,
        string expected,
        string? message = null
    )
    {
        if (actual?.StartsWith(expected, InvariantCulture) == true)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to start with {1}, but got {2}.",
            nameof(ConfirmStartsWith),
            new StringFormatter(),
            expected,
            actual,
            message
        );
    }

    public static string? ConfirmNotStartsWith(
        this string? actual,
        string expected,
        string? message = null
    )
    {
        if (actual?.StartsWith(expected, InvariantCulture) == false)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to not start with {1}, but got {2}.",
            nameof(ConfirmNotStartsWith),
            new StringFormatter(),
            expected,
            actual,
            message
        );
    }
    #endregion ConfirmStartsWith

    #region ConfirmEndsWith
    public static string? ConfirmEndsWith(
        this string? actual,
        string expected,
        string? message = null
    )
    {
        if (actual?.EndsWith(expected, InvariantCulture) == true)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to end with {1}, but got {2}.",
            nameof(ConfirmEndsWith),
            new StringFormatter(),
            expected,
            actual,
            message
        );
    }

    public static string? ConfirmNotEndsWith(
        this string? actual,
        string expected,
        string? message = null
    )
    {
        if (actual?.EndsWith(expected, InvariantCulture) == false)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to not end with {1}, but got {2}.",
            nameof(ConfirmNotEndsWith),
            new StringFormatter(),
            expected,
            actual,
            message
        );
    }
    #endregion ConfirmEndsWith

    #region ConfirmHasLength
    public static string ConfirmHasLength(
        this string actual,
        int expected,
        string? message = null
    )
    {
        if (actual.Length == expected)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to have length of {1}, but got {2}.",
            nameof(ConfirmHasLength),
            new NumericFormatter(),
            expected,
            actual.Length,
            message
        );
    }

    public static string ConfirmNotHasLength(
        this string actual,
        int expected,
        string? message = null
    )
    {
        if (actual.Length != expected)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to not have length of {1}.",
            nameof(ConfirmNotHasLength),
            new NumericFormatter(),
            expected,
            null,
            message
        );
    }
    #endregion ConfirmHasLength

    #region ConfirmEqualsCaseInsensitive
    public static string? ConfirmEqualsCaseInsensitive(
        this string? actual,
        string expected,
        string? message = null
    )
    {
        if (string.Equals(actual, expected, OrdinalIgnoreCase))
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to equal {1}, but got {2}.",
            nameof(ConfirmEqualsCaseInsensitive),
            new StringFormatter(),
            actual,
            expected,
            message
        );
    }

    public static string? ConfirmNotEqualsCaseInsensitive(
        this string? actual,
        string expected,
        string? message = null
    )
    {
        if (!string.Equals(actual, expected, OrdinalIgnoreCase))
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected string to not equal {1}.",
            nameof(ConfirmNotEqualsCaseInsensitive),
            new StringFormatter(),
            actual,
            null,
            message
        );
    }
    #endregion ConfirmEqualsCaseInsensitive

    #region ConfirmMatchesPattern
    public static string ConfirmMatchesPattern(
        this string value,
        string pattern,
        string? message = null
    )
    {
        if (Regex.IsMatch(value, pattern))
        {
            return value;
        }

        throw new ConfirmAssertException(
            "Expected string {1} to match pattern {2}.",
            nameof(ConfirmMatchesPattern),
            new StringFormatter(),
            value,
            pattern,
            message
        );
    }

    public static string ConfirmDoesNotMatchPattern(
        this string value,
        string pattern,
        string? message = null
    )
    {
        if (!Regex.IsMatch(value, pattern))
        {
            return value;
        }

        throw new ConfirmAssertException(
            "Expected string {1} to not match pattern {2}.",
            nameof(ConfirmDoesNotMatchPattern),
            new StringFormatter(),
            value,
            pattern,
            message
        );
    }
    #endregion ConfirmMatchesPattern

    public static bool ConfirmLowercase(this string value, string? message = null)
    {
        if (value.Equals(value.ToLowerInvariant(), Ordinal))
        {
            return true;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be lowercase.",
            nameof(ConfirmLowercase),
            new StringFormatter(),
            value,
            null,
            message
        );
    }

    public static bool ConfirmUppercase(this string value, string? message = null)
    {
        if (value.Equals(value.ToUpperInvariant(), Ordinal))
        {
            return true;
        }

        throw new ConfirmAssertException(
            "Expected {1} to be uppercase.",
            nameof(ConfirmUppercase),
            new StringFormatter(),
            value,
            null,
            message
        );
    }
}
