using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmTypeTest
{
	[TestCase("Lorem ipsum", typeof(string))]
	[TestCase(42, typeof(int))]
	[TestCase(3.14, typeof(double))]
	[TestCase(true, typeof(bool))]
	public static void ConfirmType_WhenOfType(object? actual, Type expected)
	{
		actual.ConfirmType(expected);
	}

	[TestCase("Lorem ipsum", typeof(int))]
	[TestCase(42, typeof(double))]
	[TestCase(3.14, typeof(bool))]
	public static void ConfirmType_WhenNotOfType(object? actual, Type expected)
	{
		Action action = () => actual.ConfirmType(expected);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	[TestCase("Lorem ipsum", typeof(int))]
	[TestCase(42, typeof(double))]
	[TestCase(3.14, typeof(bool))]
	public static void ConfirmNotType_WhenNotOfType(object? actual, Type expected)
	{
		actual.ConfirmNotType(expected);
	}

	[TestCase("Lorem ipsum", typeof(string))]
	[TestCase(42, typeof(int))]
	[TestCase(3.14, typeof(double))]
	[TestCase(true, typeof(bool))]
	public static void ConfirmNotType_WhenOfType(object? actual, Type expected)
	{
		Action action = () => actual.ConfirmNotType(expected);

		action.ConfirmThrows<ConfirmAssertException>();
	}
}
