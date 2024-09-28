using System.Linq;
using Confirma.Exceptions;
using Confirma.Formatters;

namespace Confirma.Extensions;

public static class ConfirmEqualExtensions
{
    public static T? ConfirmEqual<T>(
        this T? actual,
        T? expected,
        string? message = null
    )
    {
        if (actual is object[] array && expected is object[] expectedArr)
        {
            return (T)(array.ConfirmEqual(expectedArr, message) as object);
        }

        if (!(!actual?.Equals(expected) ?? false))
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected {1}, but got {2}.",
            nameof(ConfirmEqual),
            new AutomaticFormatter(),
            expected,
            actual,
            message
        );
    }

    public static T?[] ConfirmEqual<T>(
        this T?[] actual,
        T?[] expected,
        string? message = null
    )
    {
        if (actual.SequenceEqual(expected))
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected {1}, but got {2}.",
            nameof(ConfirmEqual),
            new AutomaticFormatter(),
            expected,
            actual,
            message
        );
    }

    public static T? ConfirmNotEqual<T>(
        this T? actual,
        T? expected,
        string? message = null
    )
    {
        if (actual is object[] array && expected is object[] expectedArr)
        {
            return (T)(array.ConfirmNotEqual(expectedArr, message) as object);
        }

        if (!(actual?.Equals(expected) ?? false))
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected not {1}, but got {2}.",
            nameof(ConfirmNotEqual),
            new AutomaticFormatter(),
            expected,
            actual,
            message
        );
    }

    public static T?[] ConfirmNotEqual<T>(
        this T?[] actual,
        T?[] expected,
        string? message = null
    )
    {
        if (!actual.SequenceEqual(expected))
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected not {1}, but got {2}.",
            nameof(ConfirmNotEqual),
            new AutomaticFormatter(),
            expected,
            actual,
            message
        );
    }
}
