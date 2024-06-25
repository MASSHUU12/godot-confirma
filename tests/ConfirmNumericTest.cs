using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

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
		Action action = () => actual.ConfirmIsPositive();

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => actual.ConfirmIsNotPositive();

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => actual.ConfirmIsNegative();

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => actual.ConfirmIsNotNegative();

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmSign
	[TestCase(-5, true)]
	[TestCase(5, false)]
	public static void ConfirmSign_WhenCorrect(int number, bool expected)
	{
		number.ConfirmSign(expected);
	}

	[TestCase(-5, false)]
	[TestCase(5, true)]
	public static void ConfirmSign_WhenIncorrect(int number, bool expected)
	{
		Action action = () => number.ConfirmSign(expected);

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => actual.ConfirmIsZero();

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion
}
