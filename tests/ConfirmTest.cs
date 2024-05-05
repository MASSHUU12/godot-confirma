using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmTest
{
	[TestCase]
	public static void ConfirmThrows_Action()
	{
		static void Test() => throw new ConfirmAssertException("Lorem ipsum dolor sit amet.");

		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(Test);
	}

	[TestCase]
	public static void ConfirmThrows_Func()
	{
		Func<object> Test = () => throw new ConfirmAssertException("Lorem ipsum dolor sit amet.");

		Test.ConfirmThrows<ConfirmAssertException>();
	}

	[TestCase]
	public static void ConfirmNotThrows_Action()
	{
		static void Test() => new object().ToString();

		ConfirmExceptionExtensions.ConfirmNotThrows<ConfirmAssertException>(Test);
	}

	[TestCase]
	public static void ConfirmNotThrows_Func()
	{
		Func<object> Test = () => new object();

		Test.ConfirmNotThrows<ConfirmAssertException>();
	}
}
