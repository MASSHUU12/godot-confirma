using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmRangeExtensions
{
	public static T ConfirmInRange<T>(this T actual, T min, T max, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo(min) < 0 || actual.CompareTo(max) > 0)
			throw new ConfirmAssertException(
				message ??
				$"Expected object to be in range [{min}, {max}], but {actual} was provided."
			);
		return actual;
	}

	public static T ConfirmNotInRange<T>(this T actual, T min, T max, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo(min) >= 0 && actual.CompareTo(max) <= 0)
			throw new ConfirmAssertException(
				message ??
				$"Expected object to be not in range [{min}, {max}], but {actual} was provided."
			);
		return actual;
	}

	public static T ConfirmGraterThan<T>(this T actual, T value, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo(value) <= 0)
			throw new ConfirmAssertException(
				message ??
				$"Expected object to be grater than {value}, but {actual} was provided."
			);
		return actual;
	}

	public static T ConfirmGraterThanOrEqual<T>(this T actual, T value, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo(value) < 0)
			throw new ConfirmAssertException(
				message ??
				$"Expected object to be grater than or equal to {value}, but {actual} was provided."
			);
		return actual;
	}

	public static T ConfirmLessThan<T>(this T actual, T value, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo(value) >= 0)
			throw new ConfirmAssertException(
				message ??
				$"Expected object to be less than {value}, but {actual} was provided."
			);
		return actual;
	}

	public static T ConfirmLessThanOrEqual<T>(this T actual, T value, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo(value) > 0)
			throw new ConfirmAssertException(
				message ??
				$"Expected object to be less than or equal to {value}, but {actual} was provided."
			);
		return actual;
	}
}
