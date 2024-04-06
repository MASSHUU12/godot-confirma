using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmNullTest
{
	[TestCase(parameters: new object?[] { null })]
	public static void ConfirmNull_WhenNull(object? actual)
	{
		actual.ConfirmNull();
	}

	// TODO: Create a test case for ConfirmNull_WhenNotNull

	[TestCase(parameters: "Lorem ipsum")]
	[TestCase(parameters: 2)]
	public static void ConfirmNotNull_WhenNotNull(object? actual)
	{
		actual.ConfirmNotNull();
	}

	// TODO: Create a test case for ConfirmNotNull_WhenNull
}
