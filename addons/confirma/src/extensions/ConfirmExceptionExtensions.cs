using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmExceptionExtensions
{
	#region ConfirmThrows
	public static Func<T> ConfirmThrows<T>(this Func<T> action, Type e, string? message = null)
	{
		try
		{
			action();
		}
		catch (Exception ex)
		{
			if (ex.GetType() == e) return action;

			throw new ConfirmAssertException(
				message ??
				$"Expected exception of type '{e.Name}' but exception of type '{ex.GetType().Name}' was thrown."
			);
		}

		throw new ConfirmAssertException(
			message ??
			$"Expected exception of type '{e.Name}' but no exception was thrown."
		);
	}

	public static Func<object?> ConfirmThrows<E>(this Func<object?> action, string? message = null)
	where E : Exception
	{
		return action.ConfirmThrows(typeof(E), message);
	}

	public static Action ConfirmThrows<E>(this Action action, string? message = null)
	{
		Func<object> func = () =>
		{
			action();
			return new object();
		};

		func.ConfirmThrows(typeof(E), message);

		return action;
	}
	#endregion

	#region ConfirmNotThrows
	public static Func<T> ConfirmNotThrows<T>(this Func<T> action, Type e, string? message = null)
	{
		try
		{
			action();
		}
		catch (Exception ex)
		{
			if (ex.GetType() == e)
			{
				throw new ConfirmAssertException(
					message ??
					$"Expected exception of type '{e.Name}' not to be thrown but it was."
				);
			}
		}

		return action;
	}

	public static Func<object?> ConfirmNotThrows<E>(this Func<object?> action, string? message = null)
	where E : Exception
	{
		return ConfirmNotThrows(action, typeof(E), message);
	}

	public static Action ConfirmNotThrows<E>(this Action action, string? message = null)
	{
		Func<object> func = () =>
		{
			action();
			return new object();
		};

		func.ConfirmNotThrows(typeof(E), message);

		return action;
	}
	#endregion

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
			action();
		}
		catch (Exception ex)
		{
			if (ex.GetType() == e && ex.Message == exMessage) return action;

			if (ex.GetType() != e && ex.Message != exMessage)
			{
				throw new ConfirmAssertException(
					message ??
					$"Expected exception of type '{e.Name}' with message '{exMessage}' " +
					$"but exception of type '{ex.GetType().Name}' was thrown {(
						string.IsNullOrEmpty(ex.Message)
						? "without a message"
						: $"with message '{ex.Message}'"
					)}."
				);
			}

			if (ex.GetType() != e)
			{
				throw new ConfirmAssertException(
					message ??
					$"Expected exception of type '{e.Name}' " +
					$"but exception of type '{ex.GetType().Name}' was thrown."
				);
			}

			if (ex.Message != exMessage)
			{
				throw new ConfirmAssertException(
					message ??
					$"Expected exception to be thrown with message '{exMessage}' " +
					$"but was thrown with message '{ex.Message}'."
				);
			}
		}

		throw new ConfirmAssertException(
			message ??
			$"Expected exception of type '{e.Name}' but no exception was thrown."
		);
	}

	public static Func<object?> ConfirmThrowsWMessage<E>(this Func<object?> action, string exMessage, string? message = null)
	where E : Exception
	{
		return action.ConfirmThrowsWMessage(typeof(E), exMessage, message);
	}

	public static Action ConfirmThrowsWMessage<E>(this Action action, string exMessage, string? message = null)
	where E : Exception
	{
		Func<object> func = () =>
		{
			action();
			return new object();
		};

		func.ConfirmThrowsWMessage(typeof(E), exMessage, message);

		return action;
	}
	#endregion
}
