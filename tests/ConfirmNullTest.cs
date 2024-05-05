using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
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
		Action action = () => actual.ConfirmNull();

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => actual.ConfirmNotNull();

		action.ConfirmThrows<ConfirmAssertException>();
	}
}
