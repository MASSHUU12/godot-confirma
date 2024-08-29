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

        return message is not null
            ? throw new ConfirmAssertException(message)
            : throw new ConfirmAssertException(
                nameof(ConfirmNull),
                "null",
                obj
            );
    }

    public static object? ConfirmNotNull(this object? obj, string? message = null)
    {
        if (obj is not null)
        {
            return obj;
        }

        return message is not null
            ? throw new ConfirmAssertException(message)
            : throw new ConfirmAssertException(
                nameof(ConfirmNotNull),
                "not-null",
                "null"
            );
    }
}
