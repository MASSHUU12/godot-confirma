using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ParametrizedTest
{
	[TestCase(1, 1, "ddd")]
	[TestCase(2, 2, 4)]
	[TestCase(3, 3, 6)]
	public static void TestEqual(int a, int b, int expected)
	{
		var actual = a + b;
		actual.ConfirmEqual(expected);
	}

	[TestCase(1, 1, 3)]
	[TestCase(2, 2, 5)]
	[TestCase(3, 3, 7)]
	public static void TestNotEqual(int a, int b, int expected)
	{
		var actual = a + b;
		actual.ConfirmNotEqual(expected);
	}
}
