using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmTest
{
	private enum TestEnum
	{
		A, B, C, D, E
	}

	#region IsEnumValue
	[TestCase(0)]
	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	[TestCase(4)]
	public static void IsEnumValue_WhenIsEnumValue(int value)
	{
		Confirm.IsEnumValue<TestEnum>(value);
	}

	[TestCase(-1)]
	[TestCase(5)]
	public static void IsEnumValue_WhenISNotEnumValue(int value)
	{
		Action action = () => Confirm.IsEnumValue<TestEnum>(value);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region IsNotEnumValue
	[TestCase(-1)]
	[TestCase(5)]
	public static void IsNotEnumValue_WhenIsNotEnumValue(int value)
	{
		Confirm.IsNotEnumValue<TestEnum>(value);
	}

	[TestCase(0)]
	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	[TestCase(4)]
	public static void IsNotEnumValue_WhenIsEnumValue(int value)
	{
		Action action = () => Confirm.IsNotEnumValue<TestEnum>(value);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region IsEnumName
	[TestCase("A", false)]
	[TestCase("a", true)]
	[TestCase("B", false)]
	[TestCase("b", true)]
	[TestCase("C", false)]
	[TestCase("c", true)]
	[TestCase("D", false)]
	[TestCase("d", true)]
	[TestCase("E", false)]
	[TestCase("e", true)]
	public static void IsEnumName_WhenIsEnumName(string name, bool ignoreCase)
	{
		Confirm.IsEnumName<TestEnum>(name, ignoreCase);
	}

	[TestCase("a")]
	[TestCase("b")]
	[TestCase("c")]
	public static void IsEnumName_WhenIsEnumNameIncorrectCase(string name)
	{
		Action action = () => Confirm.IsEnumName<TestEnum>(name);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	[TestCase("0", false)]
	[TestCase("F", false)]
	[TestCase("0", true)]
	[TestCase("f", true)]
	public static void IsEnumName_WhenIsNotEnumName(string name, bool ignoreCase)
	{
		Action action = () => Confirm.IsEnumName<TestEnum>(name, ignoreCase);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region IsTrue
	[TestCase]
	public static void IsTrue_WhenIsTrue()
	{
		Confirm.IsTrue(true);
	}

	[TestCase]
	public static void IsTrue_WhenIsFalse()
	{
		Action action = () => Confirm.IsTrue(false);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region IsFalse
	public static void IsFalse_WhenIsFalse()
	{
		Confirm.IsFalse(false);
	}

	[TestCase]
	public static void IsFalse_WhenIsTrue()
	{
		Action action = () => Confirm.IsFalse(true);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion
}
