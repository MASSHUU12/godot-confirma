using System;

namespace Confirma.Extensions;

public static class ConfirmDateTimeExtensions
{
	public static DateTime ConfirmIsBefore(this DateTime actual, DateTime dateTime, string? message = null)
	{
		return actual.ConfirmLessThan(
			dateTime,
			message ?? $"Expected {actual.ToUniversalTime()} to be before {dateTime.ToUniversalTime()}."
		);
	}

	public static DateTime ConfirmIsOnOrBefore(this DateTime actual, DateTime dateTime, string? message = null)
	{
		return actual.ConfirmLessThanOrEqual(
			dateTime,
			message ?? $"Expected {actual.ToUniversalTime()} to be on or before {dateTime.ToUniversalTime()}."
		);
	}

	public static DateTime ConfirmIsAfter(this DateTime actual, DateTime dateTime, string? message = null)
	{
		return actual.ConfirmGreaterThan(
			dateTime,
			message ?? $"Expected {actual.ToUniversalTime()} to be after {dateTime.ToUniversalTime()}."
		);
	}

	public static DateTime ConfirmIsOnOrAfter(this DateTime actual, DateTime dateTime, string? message = null)
	{
		return actual.ConfirmGreaterThanOrEqual(
			dateTime,
			message ?? $"Expected {actual.ToUniversalTime()} to be on or after {dateTime.ToUniversalTime()}."
		);
	}
}
