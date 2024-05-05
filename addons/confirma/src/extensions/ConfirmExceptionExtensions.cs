using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmExceptionExtensions
{
	#region ConfirmThrows
	public static void ConfirmThrows(this object _, Action action, Type e, string? message = null)
	{
		try
		{
			action();
		}
		catch (Exception ex)
		{
			if (ex.GetType() == e)
			{
				return;
			}

			throw new ConfirmAssertException(
				message
				?? $"Expected exception of type {e.Name} but exception of type {ex.GetType().Name} was thrown."
			);
		}

		throw new ConfirmAssertException(
			message
			?? $"Expected exception of type {e.Name} but no exception was thrown."
		);
	}

	public static void ConfirmThrows<T>(this Action action, string? message = null)
	where T : Exception
	{
		ConfirmThrows(action, action, typeof(T), message);
	}

	public static void ConfirmThrows(this object _, Func<object> action, Type e, string? message = null)
	{
		try
		{
			action();
		}
		catch (Exception ex)
		{
			if (ex.GetType() == e)
			{
				return;
			}

			throw new ConfirmAssertException(
				message
				?? $"Expected exception of type {e.Name} but exception of type {ex.GetType().Name} was thrown."
			);
		}

		throw new ConfirmAssertException(
			message
			?? $"Expected exception of type {e.Name} but no exception was thrown."
		);
	}

	public static void ConfirmThrows<T>(this Func<object> action, string? message = null)
	where T : Exception
	{
		ConfirmThrows(action, action, typeof(T), message);
	}
	#endregion

	#region ConfirmNotThrows
	public static void ConfirmNotThrows(this object _, Action action, Type e, string? message = null)
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
					message
					?? $"Expected exception of type {e.Name} not to be thrown but it was."
				);
			}
		}
	}

	public static void ConfirmNotThrows<T>(this Action action, string? message = null)
	where T : Exception
	{
		ConfirmNotThrows(action, action, typeof(T), message);
	}

	public static void ConfirmNotThrows(this object _, Func<object> action, Type e, string? message = null)
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
					message
					?? $"Expected exception of type {e.Name} not to be thrown but it was."
				);
			}
		}
	}

	public static void ConfirmNotThrows<T>(this Func<object> action, string? message = null)
	where T : Exception
	{
		ConfirmNotThrows(action, action, typeof(T), message);
	}
	#endregion
}
