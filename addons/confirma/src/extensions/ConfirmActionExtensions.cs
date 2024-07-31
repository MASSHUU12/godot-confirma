using System;
using System.Threading.Tasks;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmActionExtensions
{
    public static Action ConfirmCompletesWithin(
        this Action action,
        TimeSpan timeSpan,
        string? message = null
    )
    {
        return !Task.Run(action).Wait(timeSpan)
            ? throw new ConfirmAssertException(
                message
                ?? $"Expected action to complete within {timeSpan.TotalMilliseconds} ms, but it timed out."
            )
            : action;
    }

    public static Action ConfirmDoesNotCompleteWithin(
        this Action action,
        TimeSpan timeSpan,
        string? message = null
    )
    {
        try
        {
            _ = ConfirmCompletesWithin(action, timeSpan, message);
        }
        catch (ConfirmAssertException)
        {
            return action;
        }

        throw new ConfirmAssertException(
            message
            ?? $"Action should not have completed within {timeSpan.TotalMilliseconds} ms, but it did."
        );
    }
}
