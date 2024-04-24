using System;
using Confirma.Attributes;
using Confirma.Exceptions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmArrayTest
{
	[TestCase(new int[] { }, 0)]
	[TestCase(new int[] { 1 }, 1)]
	[TestCase(new int[] { 1, 2 }, 2)]
	public static void ConfirmSize_WhenIsOfSize(int[] array, int expectedSize)
	{
		array.ConfirmSize(expectedSize);
	}

	[TestCase(new int[] { 1 }, 0)]
	[TestCase(new int[] { 1, 2 }, 1)]
	[TestCase(new int[] { 1, 2, 3 }, 2)]
	public static void ConfirmSize_WhenIsNotOfSize(int[] array, int expectedSize)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => array.ConfirmSize(expectedSize));
	}

	[TestCase]
	public static void ConfirmEmpty_WhenIsEmpty()
	{
		Array.Empty<int>().ConfirmEmpty();
	}

	[TestCase(new int[] { 1 })]
	[TestCase(new int[] { 1, 2 })]
	[TestCase(new int[] { 1, 2, 3 })]
	public static void ConfirmEmpty_WhenIsNotEmpty(int[] array)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => array.ConfirmEmpty());
	}

	[TestCase(new int[] { 1 }, 1)]
	[TestCase(new int[] { 1, 2 }, 2)]
	[TestCase(new int[] { 1, 2, 3 }, 2)]
	public static void ConfirmContains_WhenContains(int[] array, int expected)
	{
		array.ConfirmContains(expected);
	}

	[TestCase(new int[] { 1 }, 2)]
	[TestCase(new int[] { 1, 2 }, 3)]
	[TestCase(new int[] { 1, 2, 3 }, 4)]
	public static void ConfirmContains_WhenNotContains(int[] array, int expected)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => array.ConfirmContains(expected));
	}
}
