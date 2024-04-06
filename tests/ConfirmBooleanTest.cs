using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmBooleanTest
{
	[TestCase]
	public static void ConfirmTrue_WhenTrue()
	{
		true.ConfirmTrue();
	}

	// TODO: Create a test case for ConfirmTrue_WhenFalse

	[TestCase]
	public static void ConfirmFalse_WhenFalse()
	{
		false.ConfirmFalse();
	}

	// TODO: Create a test case for ConfirmFalse_WhenTrue
}
