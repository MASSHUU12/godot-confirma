using Confirma.Attributes;
using Confirma.Exceptions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable()]
public static class ConfirmNullTest
{
	[TestCase(new object?[] { null })]
	public static void ConfirmNull_WhenNull(object? actual)
	{
		actual.ConfirmNull();
	}

	[TestCase("Lorem ipsum")]
	[TestCase(2)]
	public static void ConfirmNull_WhenNotNull(object? actual)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmNull());
	}

	[TestCase("Lorem ipsum")]
	[TestCase(2)]
	public static void ConfirmNotNull_WhenNotNull(object? actual)
	{
		actual.ConfirmNotNull();
	}

	[TestCase(new object?[] { null })]
	public static void ConfirmNotNull_WhenNull(object? actual)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmNotNull());
	}
}
