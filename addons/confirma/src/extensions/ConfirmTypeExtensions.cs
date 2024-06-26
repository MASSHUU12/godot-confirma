using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmTypeExtensions
{
	public static object? ConfirmType(this object? actual, Type expected, string? message = null)
	{
		if (actual?.GetType() == expected) return actual;

		throw new ConfirmAssertException(message ?? $"Expected object to be of type {expected}.");
	}

	public static T ConfirmType<T>(this object? actual, string? message = null)
	{
		actual.ConfirmType(typeof(T), message);
		return (T)actual!;
	}

	public static object? ConfirmNotType(this object? actual, Type expected, string? message = null)
	{
		if (actual?.GetType() != expected) return actual;

		throw new ConfirmAssertException(message ?? $"Expected object to be not of type {expected}.");
	}

	public static object? ConfirmNotType<T>(this object? actual, string? message = null)
	{
		return actual.ConfirmNotType(typeof(T), message);
	}
}
