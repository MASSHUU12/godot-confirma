using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmArrayExtensions
{
	public static T[] ConfirmSize<T>(this T[] array, int expectedSize, string? message = null)
	{
		if (array.Length == expectedSize) return array;

		throw new ConfirmAssertException(message ?? $"Array size is {array.Length} but expected {expectedSize}.");
	}

	public static T[] ConfirmEmpty<T>(this T[] array, string? message = null)
	{
		if (array.Length == 0) return array;

		throw new ConfirmAssertException(message ?? "Expected array to be empty.");
	}

	public static T[] ConfirmNotEmpty<T>(this T[] array, string? message = null)
	{
		if (array.Length > 0) return array;

		throw new ConfirmAssertException(message ?? "Expected array to not be empty.");
	}

	public static T[] ConfirmContains<T>(this T[] array, T expected, string? message = null)
	{
		if (Array.IndexOf(array, expected) != -1) return array;

		throw new ConfirmAssertException(message ?? $"Expected array to contain '{expected}'.");
	}

	public static T[] ConfirmNotContains<T>(this T[] array, T expected, string? message = null)
	{
		if (Array.IndexOf(array, expected) == -1) return array;

		throw new ConfirmAssertException(message ?? $"Expected array to not contain '{expected}'.");
	}
}
