using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmNullTest
{
	[TestCase(parameters: "Lorem ipsum")]
	[TestCase(parameters: new object?[] { null })]
	public static void ConfirmNull_Null(string? actual)
	{
		actual.ConfirmNull();
	}

	[TestCase(parameters: "Lorem ipsum")]
	[TestCase(parameters: new object?[] { null })]
	public static void ConfirmNull_NotNull(string? actual)
	{
		actual.ConfirmNull();
	}
}
