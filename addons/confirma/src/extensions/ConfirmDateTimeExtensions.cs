using System;

namespace Confirma.Extensions;

public static class ConfirmDateTimeExtensions
{
	public static DateTime ConfirmIsBefore(this DateTime actual, DateTime dateTime)
	{
		return actual.ConfirmLessThan(dateTime);
	}

	public static DateTime ConfirmIsOnOrBefore(this DateTime actual, DateTime dateTime)
	{
		return actual.ConfirmLessThanOrEqual(dateTime);
	}

	public static DateTime ConfirmIsAfter(this DateTime actual, DateTime dateTime)
	{
		return actual.ConfirmGreaterThan(dateTime);
	}

	public static DateTime ConfirmIsOnOrAfter(this DateTime actual, DateTime dateTime)
	{
		return actual.ConfirmGreaterThanOrEqual(dateTime);
	}
}
