using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmBooleanExtensions
{
    public static bool ConfirmTrue(this bool actual, string? message = null)
    {
        if (actual)
        {
            return actual;
        }

        return message is not null
            ? throw new ConfirmAssertException(message)
            : throw new ConfirmAssertException(
                nameof(ConfirmTrue),
                true,
                actual
            );
    }

    public static bool ConfirmFalse(this bool actual, string? message = null)
    {
        if (!actual)
        {
            return actual;
        }

        return message is not null
            ? throw new ConfirmAssertException(message)
            : throw new ConfirmAssertException(
                nameof(ConfirmFalse),
                false,
                actual
            );
    }
}
