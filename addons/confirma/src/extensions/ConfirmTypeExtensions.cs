using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmTypeExtensions
{
    public static object? ConfirmType(
        this object? actual,
        Type expected,
        string? message = null
    )
    {
        if (actual?.GetType() == expected)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected object of type {1}, but got {2}.",
            nameof(ConfirmType),
            null,
            expected.Name,
            actual?.GetType().Name,
            message
        );
    }

    public static T ConfirmType<T>(this object? actual, string? message = null)
    {
        _ = actual.ConfirmType(typeof(T), message);
        return (T)actual!;
    }

    public static object? ConfirmNotType(
        this object? actual,
        Type expected,
        string? message = null
    )
    {
        if (actual?.GetType() != expected)
        {
            return actual;
        }

        throw new ConfirmAssertException(
            "Expected object not to be of type {1}.",
            nameof(ConfirmNotType),
            null,
            expected.Name,
            null,
            message
        );
    }

    public static object? ConfirmNotType<T>(
        this object? actual,
        string? message = null
    )
    {
        return actual.ConfirmNotType(typeof(T), message);
    }
}
