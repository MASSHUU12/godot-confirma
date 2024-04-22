using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
[Parallelizable()]
public static class ConfirmBooleanTest
{
	[TestCase]
	public static void ConfirmTrue_WhenTrue()
	{
		true.ConfirmTrue();
	}

	[TestCase]
	public static void ConfirmTrue_WhenFalse()
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => false.ConfirmTrue());
	}

	[TestCase]
	public static void ConfirmFalse_WhenFalse()
	{
		false.ConfirmFalse();
	}

	[TestCase]
	public static void ConfirmFalse_WhenTrue()
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => true.ConfirmFalse());
	}
}
