using System;
using Confirma.Exceptions;

namespace Confirma;

public static class ConfirmNumericExtension
{
	public static void ConfirmIsPositive<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) <= 0)
			throw new ConfirmAssertException(message ?? "Expected object to be positive.");
	}

	public static void ConfirmIsNotPositive<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) > 0)
			throw new ConfirmAssertException(message ?? "Expected object to not be positive.");
	}

	public static void ConfirmIsNegative<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) >= 0)
			throw new ConfirmAssertException(message ?? "Expected object to be negative.");
	}

	public static void ConfirmIsNotNegative<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) < 0)
			throw new ConfirmAssertException(message ?? "Expected object to not be negative.");
	}

	public static void ConfirmIsZero<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) != 0)
			throw new ConfirmAssertException(message ?? "Expected object to be zero.");
	}

	public static void ConfirmIsNotZero<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) == 0)
			throw new ConfirmAssertException(message ?? "Expected object to not be zero.");
	}
}
