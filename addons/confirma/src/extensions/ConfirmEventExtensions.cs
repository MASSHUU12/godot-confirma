using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmEventExtensions
{
	public static Action ConfirmRaisesEvent<TEventArgs>(
		this Action action,
		ref EventHandler<TEventArgs>? eventHandler,
		string? message = null
	)
	where TEventArgs : EventArgs
	{
		bool eventRaised = false;
		void Handler(object? sender, TEventArgs e) => eventRaised = true;

		eventHandler += Handler;

		try
		{
			action();
		}
		finally
		{
			eventHandler -= eventHandler is null ? null : Handler;
		}

		if (!eventRaised)
		{
			throw new ConfirmAssertException(
				message ??
				$"Expected {eventHandler?.GetType().Name ?? "event"} to be raised, but it was not."
			);
		}

		return action;
	}

	public static Action ConfirmDoesNotRaiseEvent<TEventArgs>(
		this Action action,
		ref EventHandler<TEventArgs>? eventHandler,
		string? message = null
	)
	where TEventArgs : EventArgs
	{
		bool eventRaised = false;
		void Handler(object? sender, TEventArgs e) => eventRaised = true;

		eventHandler += Handler;

		try
		{
			action();
		}
		finally
		{
			eventHandler -= Handler;
		}

		if (eventRaised)
		{
			throw new ConfirmAssertException(
				message ??
				$"Expected {eventHandler?.GetType().Name ?? "event"} to not be raised, but it was."
			);
		}

		return action;
	}
}
