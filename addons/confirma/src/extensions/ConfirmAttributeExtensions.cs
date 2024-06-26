using System;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmAttributeExtensions
{
	#region ConfirmIsDecoratedWith
	public static Type ConfirmIsDecoratedWith(this Type actual, Type expected, string? message = null)
	{
		if (actual.IsDefined(expected, false)) return actual;

		throw new ConfirmAssertException(message ?? $"Expected '{actual.Name}' to be decorated with '{expected.Name}' but was not.");
	}

	public static Type ConfirmIsDecoratedWith<T>(this Type actual, string? message = null)
		where T : Attribute
	{
		return ConfirmIsDecoratedWith(actual, typeof(T), message);
	}
	#endregion

	#region ConfirmIsNotDecoratedWith
	public static Type ConfirmIsNotDecoratedWith(this Type actual, Type expected, string? message = null)
	{
		if (!actual.IsDefined(expected, false)) return actual;

		throw new ConfirmAssertException(message ?? $"Expected '{actual.Name}' to not be decorated with '{expected.Name}' but was.");
	}

	public static Type ConfirmIsNotDecoratedWith<T>(this Type actual, string? message = null)
		where T : Attribute
	{
		return ConfirmIsNotDecoratedWith(actual, typeof(T), message);
	}
	#endregion
}
