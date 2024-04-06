using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class BasicTest
{
	[TestCase]
	public static void Test1()
	{
		true.ConfirmFalse();
	}

	[TestCase]
	public static void Test2()
	{
		false.ConfirmFalse();
	}

	[TestCase]
	[Ignore("Reason")]
	public static void Test3()
	{
		true.ConfirmTrue();
	}
}
