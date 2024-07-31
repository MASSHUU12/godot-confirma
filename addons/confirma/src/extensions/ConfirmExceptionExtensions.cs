using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmExceptionExtensions
{
    #region ConfirmThrows
    public static Func<T> ConfirmThrows<T>(
        this Func<T> action,
        Type e,
        string? message = null
    )
    {
        try
        {
            _ = action();
        }
        catch (Exception ex)
        {
            if (ex.GetType() == e)
            {
                return action;
            }

            throw new ConfirmAssertException(
                message
                ?? $"Expected {e.Name} exception, but got {ex.GetType().Name} instead."
            );
        }

        throw new ConfirmAssertException(
            message
            ?? $"Expected {e.Name} exception, but no exception was thrown."
        );
    }

    public static Func<object?> ConfirmThrows<TE>(
        this Func<object?> action,
        string? message = null
    )
    where TE : Exception
    {
        return action.ConfirmThrows(typeof(TE), message);
    }

    public static Action ConfirmThrows<TE>(
        this Action action,
        string? message = null
    )
    {
        Func<object> func = () =>
        {
            action();
            return new object();
        };

        _ = func.ConfirmThrows(typeof(TE), message);

        return action;
    }
    #endregion ConfirmThrows

    #region ConfirmNotThrows
    public static Func<T> ConfirmNotThrows<T>(
        this Func<T> action,
        Type e,
        string? message = null
    )
    {
        try
        {
            _ = action();
        }
        catch (Exception ex)
        {
            if (ex.GetType() == e)
            {
                throw new ConfirmAssertException(
                    message
                    ?? $"Did not expect {e.Name} exception, but it was thrown."
                );
            }
        }

        return action;
    }

    public static Func<object?> ConfirmNotThrows<TE>(
        this Func<object?> action,
        string? message = null
    )
    where TE : Exception
    {
        return ConfirmNotThrows(action, typeof(TE), message);
    }

    public static Action ConfirmNotThrows<TE>(
        this Action action,
        string? message = null
    )
    {
        Func<object> func = () =>
        {
            action();
            return new object();
        };

        _ = func.ConfirmNotThrows(typeof(TE), message);

        return action;
    }
    #endregion ConfirmNotThrows

    #region ConfirmThrowsWMessage
    public static Func<T> ConfirmThrowsWMessage<T>(
        this Func<T> action,
        Type e,
        string exMessage,
        string? message = null
    )
    {
        try
        {
            _ = action();
        }
        catch (Exception ex)
        {
            if (ex.GetType() == e && ex.Message == exMessage)
            {
                return action;
            }

            if (ex.GetType() != e && ex.Message != exMessage)
            {
                throw new ConfirmAssertException(
                    message
                    ?? $"Expected {e.Name} exception with message '{exMessage}', "
                    + $"but got {ex.GetType().Name} exception {(
                        string.IsNullOrEmpty(ex.Message)
                        ? "without a message"
                        : $"with message '{ex.Message}'"
                    )} instead."
                );
            }

            if (ex.GetType() != e)
            {
                throw new ConfirmAssertException(
                    message
                    ?? $"Expected {e.Name} exception, but got {ex.GetType().Name} exception instead."
                );
            }

            if (ex.Message != exMessage)
            {
                throw new ConfirmAssertException(
                    message
                    ?? $"Expected exception to be thrown with message '{exMessage}', "
                    + $"but got message '{ex.Message}' instead."
                );
            }
        }

        throw new ConfirmAssertException(
            message
            ?? $"Expected {e.Name} exception, but no exception was thrown."
        );
    }

    public static Func<object?> ConfirmThrowsWMessage<TE>(
        this Func<object?> action,
        string exMessage,
        string? message = null
    )
    where TE : Exception
    {
        return action.ConfirmThrowsWMessage(typeof(TE), exMessage, message);
    }

    public static Action ConfirmThrowsWMessage<TE>(
        this Action action,
        string exMessage,
        string? message = null
    )
    where TE : Exception
    {
        Func<object> func = () =>
        {
            action();
            return new object();
        };

        _ = func.ConfirmThrowsWMessage(typeof(TE), exMessage, message);

        return action;
    }
    #endregion ConfirmThrowsWMessage

    #region ConfirmNotThrowsWMessage
    public static Func<T> ConfirmNotThrowsWMessage<T>(
        this Func<T> action,
        Type e,
        string exMessage,
        string? message = null
    )
    {
        try
        {
            _ = action();
        }
        catch (Exception ex)
        {
            if (ex.GetType() == e)
            {
                throw new ConfirmAssertException(
                    message
                    ?? $"Did not expect {e.Name} exception with message '{exMessage}', but it was thrown."
                );
            }
        }

        return action;
    }

    public static Func<object?> ConfirmNotThrowsWMessage<TE>(
        this Func<object?> action,
        string exMessage,
        string? message = null
    )
    where TE : Exception
    {
        return ConfirmNotThrowsWMessage(action, typeof(TE), exMessage, message);
    }

    public static Action ConfirmNotThrowsWMessage<TE>(
        this Action action,
        string exMessage,
        string? message = null
    )
    where TE : Exception
    {
        Func<object> func = () =>
        {
            action();
            return new object();
        };

        _ = func.ConfirmNotThrowsWMessage(typeof(TE), exMessage, message);

        return action;
    }
    #endregion ConfirmNotThrowsWMessage
}
