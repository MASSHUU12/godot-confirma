using Confirma.Exceptions;
using Godot;

namespace Confirma.Extensions;

public static class ConfirmSignalExtensions
{
	#region ConfirmSignalExists
	public static GodotObject ConfirmSignalExists(this GodotObject actual, StringName signalName, string? message = null)
	{
		if (actual.HasSignal(signalName)) return actual;
		throw new ConfirmAssertException(message ?? $"Expected signal '{signalName}' to exist on '{actual}' but it does not.");
	}

	public static GodotObject ConfirmSignalDoesNotExist(this GodotObject actual, StringName signalName, string? message = null)
	{
		if (!actual.HasSignal(signalName)) return actual;
		throw new ConfirmAssertException(message ?? $"Expected signal '{signalName}' to not exist on '{actual}' but it does.");
	}
	#endregion
}
