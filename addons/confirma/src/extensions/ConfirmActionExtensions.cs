using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmActionExtensions
{
	public static Action ConfirmCompletesWithin(this Action action, TimeSpan timeSpan, string? message = null)
	{
		var task = System.Threading.Tasks.Task.Run(action);
		if (!task.Wait(timeSpan))
		{
			throw new ConfirmAssertException(
				message ??
				$"Expected action to complete within {timeSpan.TotalMilliseconds} ms, but it did not."
			);
		}

		return action;
	}

	public static Action ConfirmDoesNotCompleteWithin(this Action action, TimeSpan timeSpan, string? message = null)
	{
		try
		{
			ConfirmCompletesWithin(action, timeSpan, message);
		}
		catch (ConfirmAssertException)
		{
			return action;
		}

		throw new ConfirmAssertException(
			message ??
			$"Expected action to not complete within {timeSpan.TotalMilliseconds} ms, but it did."
		);
	}
}
