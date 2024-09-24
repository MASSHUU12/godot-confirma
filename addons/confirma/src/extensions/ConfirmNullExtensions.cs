using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmNullExtensions
{
    public static object? ConfirmNull(this object? obj, string? message = null)
    {
        if (obj is null)
        {
            return obj;
        }

        throw new ConfirmAssertException(
            "Expected null but got {2}.",
            nameof(ConfirmNull),
            null,
            null,
            obj, // TODO: Implement automatic formatter
            message
        );
    }

    public static object? ConfirmNotNull(this object? obj, string? message = null)
    {
        if (obj is not null)
        {
            return obj;
        }

        throw new ConfirmAssertException(
            "Expected non-null value.",
            nameof(ConfirmNotNull),
            null,
            obj, // TODO: Implement automatic formatter
            null,
            message
        );
    }
}
