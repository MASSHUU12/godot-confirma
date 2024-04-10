using System;
using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmTest
{
	[TestCase]
	public static void ConfirmThrows_Action()
	{
		static void Test() => throw new ConfirmAssertException("Lorem ipsum dolor sit amet.");

		Confirm.ConfirmThrows<ConfirmAssertException>(Test);
	}

	[TestCase]
	public static void ConfirmThrows_Func()
	{
		Func<object> Test = () => throw new ConfirmAssertException("Lorem ipsum dolor sit amet.");

		Confirm.ConfirmThrows<ConfirmAssertException>(Test);
	}

	[TestCase]
	public static void ConfirmNotThrows_Action()
	{
		static void Test() => new object().ToString();

		Confirm.ConfirmNotThrows<ConfirmAssertException>(Test);
	}

	[TestCase]
	public static void ConfirmNotThrows_Func()
	{
		Func<object> Test = () => new object();

		Confirm.ConfirmNotThrows<ConfirmAssertException>(Test);
	}
}
