using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmReferenceExtensions
{
	public static object ConfirmSameReference(this object obj1, object obj2, string? message = "")
	{
		if (ReferenceEquals(obj1, obj2)) return obj1;

		throw new ConfirmAssertException(message ?? "Expected same reference, but got different instances.");
	}

	public static object ConfirmDifferentReference(this object obj1, object obj2, string? message = "")
	{
		if (!ReferenceEquals(obj1, obj2)) return obj1;

		throw new ConfirmAssertException(message ?? "Expected different references, but got same instance.");
	}
}
