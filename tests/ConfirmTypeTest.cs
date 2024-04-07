using System;
using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmTypeTest
{
	[TestCase(parameters: new object[] { "Lorem ipsum", typeof(string) })]
	[TestCase(42, typeof(int))]
	[TestCase(3.14, typeof(double))]
	[TestCase(true, typeof(bool))]
	public static void ConfirmType_WhenOfType(object? actual, Type expected)
	{
		actual.ConfirmType(expected);
	}

	// TODO: Create a test case for ConfirmType_WhenNotOfType

	[TestCase(parameters: new object[] { "Lorem ipsum", typeof(int) })]
	[TestCase(42, typeof(double))]
	[TestCase(3.14, typeof(bool))]
	public static void ConfirmNotType_WhenNotOfType(object? actual, Type expected)
	{
		actual.ConfirmNotType(expected);
	}

	// TODO: Create a test case for ConfirmNotType_WhenOfType
}
