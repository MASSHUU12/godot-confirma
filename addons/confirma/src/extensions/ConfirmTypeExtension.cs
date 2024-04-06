namespace Confirma;

public static class ConfirmTypeExtension
{
	public static void ConfirmType<T>(this object? actual, string? message = null)
	{
		if (actual is not T)
			throw new ConfirmAssertException(message ?? $"Expected object to be of type {typeof(T)}.");
	}

	public static T ConfirmType<T>(this object? actual, T expected, string? message = null)
	{
		actual.ConfirmType<T>(message);
		return (T)actual!;
	}

	public static void ConfirmNotType<T>(this object? actual, string? message = null)
	{
		if (actual is T)
			throw new ConfirmAssertException(message ?? $"Expected object to be not of type {typeof(T)}.");
	}
}
