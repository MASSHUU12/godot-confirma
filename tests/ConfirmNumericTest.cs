using Confirma.Attributes;
using Confirma.Exceptions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmNumericTest
{
	#region ConfirmIsPositive
	[TestCase(1f)]
	[TestCase(0.1f)]
	public static void ConfirmIsPositive_WhenPositive(float actual)
	{
		actual.ConfirmIsPositive();
	}

	[TestCase(0f)]
	[TestCase(-1f)]
	public static void ConfirmIsPositive_WhenNotPositive(float actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmIsPositive());
	}
	#endregion

	#region ConfirmIsNotPositive
	[TestCase(0f)]
	[TestCase(-1f)]
	public static void ConfirmIsNotPositive_WhenNotPositive(float actual)
	{
		actual.ConfirmIsNotPositive();
	}

	[TestCase(1f)]
	[TestCase(0.1f)]
	public static void ConfirmIsNotPositive_WhenPositive(float actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmIsNotPositive());
	}
	#endregion

	#region ConfirmIsNegative
	[TestCase(-1f)]
	[TestCase(-0.1f)]
	public static void ConfirmIsNegative_WhenNegative(float actual)
	{
		actual.ConfirmIsNegative();
	}

	[TestCase(0f)]
	[TestCase(1f)]
	public static void ConfirmIsNegative_WhenNotNegative(float actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmIsNegative());
	}
	#endregion

	#region ConfirmIsNotNegative
	[TestCase(0f)]
	[TestCase(1f)]
	public static void ConfirmIsNotNegative_WhenNotNegative(float actual)
	{
		actual.ConfirmIsNotNegative();
	}

	[TestCase(-1f)]
	[TestCase(-0.1f)]
	public static void ConfirmIsNotNegative_WhenNegative(float actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmIsNotNegative());
	}
	#endregion

	#region ConfirmIsZero
	[TestCase(0f)]
	public static void ConfirmIsZero_WhenZero(float actual)
	{
		actual.ConfirmIsZero();
	}

	[TestCase(1f)]
	[TestCase(-1f)]
	public static void ConfirmIsZero_WhenNotZero(float actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmIsZero());
	}
	#endregion
}
