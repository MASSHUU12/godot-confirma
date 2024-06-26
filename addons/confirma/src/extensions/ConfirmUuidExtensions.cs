using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmUuidExtensions
{
	public static void ConfirmValidUuid4(this string? actual, string? message = null)
	{
		if (Guid.TryParse(actual, out var _)) return;

		throw new ConfirmAssertException(
			message ??
			$"Expected string to be a valid UUID, but '{actual}' was provided."
		);
	}

	public static void ConfirmInvalidUuid4(this string? actual, string? message = null)
	{
		if (!Guid.TryParse(actual, out var _)) return;

		throw new ConfirmAssertException(
			message ??
			$"Expected string to be not a valid UUID, but '{actual}' was provided."
		);
	}
}
