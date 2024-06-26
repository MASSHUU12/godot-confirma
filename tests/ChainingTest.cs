using System;
using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ChainingTest
{
	[Parallelizable]
	private static class TestClass { }

	[TestCase]
	public static void ChainingOnArray()
	{
		new int[] { 0, 1, 2, 3, 4 }.ConfirmNotEmpty().ConfirmContains(2);
	}

	[TestCase]
	public static void ChainingOnAttribute()
	{
		typeof(TestClass)
			.ConfirmIsDecoratedWith<ParallelizableAttribute>()
			.ConfirmIsNotDecoratedWith<TestClassAttribute>();
	}

	[TestCase]
	public static void ChainingOnBoolean()
	{
		true.ConfirmTrue().ConfirmTrue(); // XD
	}

	[TestCase]
	public static void ChainingOnDictionary()
	{
		new Dictionary<string, int>()
		{
			{"key1", 0},
			{"key2", 0},
			{"key3", 0},
		}
		.ConfirmContainsKey("key1")
		.ConfirmNotContainsKey("key4")
		.ConfirmNotContainsValue(1);
	}

	[TestCase]
	public static void ChainingOnEqual()
	{
		5.ConfirmEqual(5).ConfirmNotEqual(6);
	}

	[TestCase]
	public static void ChainingOnException()
	{
		Action action = () => throw new IndexOutOfRangeException();

		action
			.ConfirmThrows<IndexOutOfRangeException>()
			.ConfirmNotThrows<OutOfMemoryException>();
	}

	[TestCase]
	public static void ChainingOnFile()
	{
		new StringName("./LICENSE")
			.ConfirmIsFile()
			.ConfirmFileContains("MIT License");
	}

	[TestCase]
	public static void ChainingOnIEnumerable()
	{
		new int[] { 0, 1, 2, 3, 4 }
			.ConfirmNotEmpty()
			.ConfirmContains(2)
			.ConfirmElementsAreUnique();
	}

	[TestCase]
	public static void ChainingOnConfirmNull()
	{
		5.ConfirmNotNull().ConfirmNotNull(); // XD
	}

	[TestCase]
	public static void ChainingOnNumeric()
	{
		5.ConfirmIsNotZero().ConfirmIsPositive();
	}

	[TestCase]
	public static void ChainingOnRange()
	{
		5.ConfirmGraterThan(3).ConfirmInRange(0, 10);
	}

	[TestCase]
	public static void ChainingOnSignal()
	{
		(
			(Button)new Button()
			.ConfirmSignalExists("pressed")
			.ConfirmSignalDoesNotExist("Lorem ipsum")
		).QueueFree();
	}

	[TestCase]
	public static void ChainingOnString()
	{
		"Lorem ipsum dolor sit amet"
			.ConfirmStartsWith("Lorem")
			.ConfirmEndsWith("amet");
	}
}
