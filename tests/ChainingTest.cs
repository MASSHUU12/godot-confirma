using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Extensions;

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
	public static void ChainingOnNumeric()
	{
		5.ConfirmIsNotZero().ConfirmIsPositive();
	}

	[TestCase]
	public static void ChainingOnRange()
	{
		5.ConfirmGraterThan(3).ConfirmInRange(0, 10);
	}
}
