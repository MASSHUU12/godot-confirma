using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
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
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => false.ConfirmTrue());
	}

	[TestCase]
	public static void ConfirmFalse_WhenFalse()
	{
		false.ConfirmFalse();
	}

	[TestCase]
	public static void ConfirmFalse_WhenTrue()
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => true.ConfirmFalse());
	}
}
