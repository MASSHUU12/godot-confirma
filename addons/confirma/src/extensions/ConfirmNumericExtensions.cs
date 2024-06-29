using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmNumericExtensions
{
	#region ConfirmIsPositive
	/// <remarks>
	/// Zero is not considered positive.
	/// </remarks>
	public static T ConfirmIsPositive<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) > 0) return actual;

		throw new ConfirmAssertException(
			message ??
			$"Expected {actual} to be positive."
		);
	}

	public static T ConfirmIsNotPositive<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) <= 0) return actual;

		throw new ConfirmAssertException(
			message ??
			$"Expected {actual} to not be positive."
		);
	}
	#endregion

	#region ConfirmIsNegative
	/// <remarks>
	/// Zero is not considered negative.
	/// </remarks>
	public static T ConfirmIsNegative<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) < 0) return actual;

		throw new ConfirmAssertException(
			message ??
			$"Expected {actual} to be negative."
		);
	}

	public static T ConfirmIsNotNegative<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) >= 0) return actual;

		throw new ConfirmAssertException(
			message ??
			$"Expected {actual} to not be negative."
		);
	}
	#endregion

	#region ConfirmSign
	/// <remarks>
	/// Zero is not considered signed or unsigned.
	/// </remarks>
	public static T ConfirmSign<T>(this T actual, bool sign, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (sign && actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) < 0) return actual;
		if (!sign && actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) > 0) return actual;

		throw new ConfirmAssertException(
			message ??
			$"Expected {actual} to have a {(sign ? "negative" : "positive")} sign."
		);
	}
	#endregion

	#region ConfirmIsZero
	public static T ConfirmIsZero<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) == 0) return actual;

		throw new ConfirmAssertException(
			message ??
			$"Expected {actual} to be zero."
		);
	}

	public static T ConfirmIsNotZero<T>(this T actual, string? message = null)
	where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
	{
		if (actual.CompareTo((T)Convert.ChangeType(0, typeof(T))) != 0) return actual;

		throw new ConfirmAssertException(
			message ??
			$"Expected {actual} to not be zero."
		);
	}
	#endregion
}
