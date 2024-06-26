using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmNullExtensions
{
	public static object? ConfirmNull(this object? obj, string? message = null)
	{
		if (obj is null) return obj;

		throw new ConfirmAssertException(message ?? "Expected object to be null but it was not.");
	}

	public static object? ConfirmNotNull(this object? obj, string? message = null)
	{
		if (obj is not null) return obj;

		throw new ConfirmAssertException(message ?? "Expected object to be not null but it was.");
	}
}
