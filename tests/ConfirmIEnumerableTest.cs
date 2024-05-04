using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmIEnumerableTest
{
	#region ConfirmEmpty
	[TestCase(new int[] { })]
	public static void ConfirmEmpty_WhenEmpty(IEnumerable<int> actual)
	{
		actual.ConfirmEmpty();
	}

	[TestCase(new int[] { 1 })]
	[TestCase(new int[] { 1, 2 })]
	[TestCase(new int[] { 1, 2, 3 })]
	public static void ConfirmEmpty_WhenNotEmpty(IEnumerable<int> actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmEmpty());
	}
	#endregion

	#region ConfirmNotEmpty
	[TestCase(new int[] { 1 })]
	[TestCase(new int[] { 1, 2 })]
	[TestCase(new int[] { 1, 2, 3 })]
	public static void ConfirmNotEmpty_WhenNotEmpty(IEnumerable<int> actual)
	{
		actual.ConfirmNotEmpty();
	}

	[TestCase(new int[] { })]
	public static void ConfirmNotEmpty_WhenEmpty(IEnumerable<int> actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmNotEmpty());
	}
	#endregion

	#region ConfirmCount
	[TestCase(new float[] { }, 0)]
	[TestCase(new float[] { 1.0f }, 1)]
	[TestCase(new float[] { 1.0f, 2.0f }, 2)]
	[TestCase(new float[] { 1.0f, 2.0f, 3.0f }, 3)]
	public static void ConfirmCount_WhenEqual(IEnumerable<float> actual, int expected)
	{
		actual.ConfirmCount(expected);
	}

	[TestCase(new float[] { }, 1)]
	[TestCase(new float[] { 1.0f }, 0)]
	[TestCase(new float[] { 1.0f, 2.0f }, 1)]
	[TestCase(new float[] { 1.0f, 2.0f, 3.0f }, 2)]
	public static void ConfirmCount_WhenNotEqual(IEnumerable<float> actual, int expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmCount(expected));
	}
	#endregion

	#region ConfirmCountGreaterThan
	[TestCase(new double[] { 1.0 }, 0)]
	[TestCase(new double[] { 1.0, 2.0 }, 1)]
	[TestCase(new double[] { 1.0, 2.0, 3.0 }, 2)]
	public static void ConfirmCountGreaterThan_WhenGreaterThan(IEnumerable<double> actual, int expected)
	{
		actual.ConfirmCountGreaterThan(expected);
	}

	[TestCase(new double[] { }, 0)]
	[TestCase(new double[] { 1.0 }, 1)]
	[TestCase(new double[] { 1.0, 2.0 }, 2)]
	[TestCase(new double[] { 1.0, 2.0, 3.0 }, 3)]
	public static void ConfirmCountGreaterThan_WhenNotGreaterThan(IEnumerable<double> actual, int expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmCountGreaterThan(expected));
	}
	#endregion

	#region ConfirmCountLessThan
	[TestCase(new short[] { }, 1)]
	[TestCase(new short[] { 1 }, 2)]
	[TestCase(new short[] { 1, 2 }, 3)]
	public static void ConfirmCountLessThan_WhenLessThan(IEnumerable<short> actual, int expected)
	{
		actual.ConfirmCountLessThan(expected);
	}

	[TestCase(new short[] { 1 }, 0)]
	[TestCase(new short[] { 1, 2 }, 1)]
	[TestCase(new short[] { 1, 2, 3 }, 2)]
	[TestCase(new short[] { 1, 2, 3, 4 }, 3)]
	public static void ConfirmCountLessThan_WhenNotLessThan(IEnumerable<short> actual, int expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmCountLessThan(expected));
	}
	#endregion

	#region ConfirmCountGreaterThanOrEqual
	[TestCase(new int[] { }, 0)]
	[TestCase(new int[] { 1 }, 0)]
	[TestCase(new int[] { 1, 2 }, 1)]
	[TestCase(new int[] { 1, 2, 3 }, 2)]
	public static void ConfirmCountGreaterThanOrEqual_WhenGreaterThanOrEqual(IEnumerable<int> actual, int expected)
	{
		actual.ConfirmCountGreaterThanOrEqual(expected);
	}

	[TestCase(new int[] { }, 1)]
	[TestCase(new int[] { 1 }, 2)]
	[TestCase(new int[] { 1, 2 }, 3)]
	[TestCase(new int[] { 1, 2, 3 }, 4)]
	public static void ConfirmCountGreaterThanOrEqual_WhenNotGreaterThanOrEqual(IEnumerable<int> actual, int expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmCountGreaterThanOrEqual(expected));
	}
	#endregion

	#region ConfirmCountLessThanOrEqual
	[TestCase(new long[] { }, 1)]
	[TestCase(new long[] { 1 }, 1)]
	[TestCase(new long[] { 1, 2 }, 2)]
	[TestCase(new long[] { 1, 2, 3 }, 3)]
	public static void ConfirmCountLessThanOrEqual_WhenLessThanOrEqual(IEnumerable<long> actual, int expected)
	{
		actual.ConfirmCountLessThanOrEqual(expected);
	}

	[TestCase(new long[] { 1 }, 0)]
	[TestCase(new long[] { 1, 2 }, 1)]
	[TestCase(new long[] { 1, 2, 3 }, 2)]
	[TestCase(new long[] { 1, 2, 3, 4 }, 3)]
	public static void ConfirmCountLessThanOrEqual_WhenNotLessThanOrEqual(IEnumerable<long> actual, int expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmCountLessThanOrEqual(expected));
	}
	#endregion

	#region ConfirmContains
	[TestCase(new string[] { "a", "b", "c" }, "a")]
	[TestCase(new string[] { "a", "b", "c" }, "b")]
	[TestCase(new string[] { "a", "b", "c" }, "c")]
	public static void ConfirmContains_WhenContains(IEnumerable<string> actual, string expected)
	{
		actual.ConfirmContains(expected);
	}

	[TestCase(new string[] { "a", "b", "c" }, "d")]
	[TestCase(new string[] { "a", "b", "c" }, "e")]
	[TestCase(new string[] { "a", "b", "c" }, "f")]
	public static void ConfirmContains_WhenNotContains(IEnumerable<string> actual, string expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmContains(expected));
	}
	#endregion

	#region ConfirmNotContains
	[TestCase(new string[] { "a", "b", "c" }, "d")]
	[TestCase(new string[] { "a", "b", "c" }, "e")]
	[TestCase(new string[] { "a", "b", "c" }, "f")]
	public static void ConfirmNotContains_WhenNotContains(IEnumerable<string> actual, string expected)
	{
		actual.ConfirmNotContains(expected);
	}

	[TestCase(new string[] { "a", "b", "c" }, "a")]
	[TestCase(new string[] { "a", "b", "c" }, "b")]
	[TestCase(new string[] { "a", "b", "c" }, "c")]
	public static void ConfirmNotContains_WhenContains(IEnumerable<string> actual, string expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmNotContains(expected));
	}
	#endregion

	#region ConfirmAllMatch
	[TestCase(new int[] { 1, 2, 3 })]
	[TestCase(new int[] { 1, 2, 3, 4 })]
	[TestCase(new int[] { 1, 2, 3, 4, 5 })]
	public static void ConfirmAllMatch_WhenAllMatch(IEnumerable<int> actual)
	{
		actual.ConfirmAllMatch(x => x > 0);
	}

	[TestCase(new int[] { 1, 2, 3 })]
	[TestCase(new int[] { 1, 2, 3, 4 })]
	[TestCase(new int[] { 1, 2, 3, 4, 5 })]
	public static void ConfirmAllMatch_WhenNotAllMatch(IEnumerable<int> actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmAllMatch(x => x < 0));
	}
	#endregion

	#region ConfirmAnyMatch
	[TestCase(new int[] { 1, 2, 3 })]
	[TestCase(new int[] { 1, 2, 3, 4 })]
	[TestCase(new int[] { 1, 2, 3, 4, 5 })]
	public static void ConfirmAnyMatch_WhenAnyMatch(IEnumerable<int> actual)
	{
		actual.ConfirmAnyMatch(x => x == 3);
	}

	[TestCase(new int[] { 1, 2, 3 })]
	[TestCase(new int[] { 1, 2, 3, 4 })]
	[TestCase(new int[] { 1, 2, 3, 4, 5 })]
	public static void ConfirmAnyMatch_WhenNotAnyMatch(IEnumerable<int> actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmAnyMatch(x => x == 0));
	}
	#endregion

	#region ConfirmNoneMatch
	[TestCase(new float[] { 1.0f, 2.0f, 3.0f })]
	[TestCase(new float[] { 1.0f, 2.0f, 3.0f, 4.0f })]
	[TestCase(new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f })]
	public static void ConfirmNoneMatch_WhenNoneMatch(IEnumerable<float> actual)
	{
		actual.ConfirmNoneMatch(x => x < 0);
	}

	[TestCase(new float[] { 1.0f, 2.0f, 3.0f })]
	[TestCase(new float[] { 1.0f, 2.0f, 3.0f, 4.0f })]
	[TestCase(new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f })]
	public static void ConfirmNoneMatch_WhenNotNoneMatch(IEnumerable<float> actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmNoneMatch(x => x > 0));
	}
	#endregion

	#region ConfirmElementsAreUnique
	[TestCase(new char[] { 'a', 'b', 'c' })]
	[TestCase(new char[] { 'a', 'b', 'c', 'd' })]
	[TestCase(new char[] { 'a', 'b', 'c', 'd', 'e' })]
	public static void ConfirmElementsAreUnique_WhenUnique(IEnumerable<char> actual)
	{
		actual.ConfirmElementsAreUnique();
	}

	[TestCase(new char[] { 'a', 'a', 'c' })]
	[TestCase(new char[] { 'a', 'b', 'b', 'd' })]
	[TestCase(new char[] { 'a', 'b', 'c', 'c', 'e' })]
	public static void ConfirmElementsAreUnique_WhenNotUnique(IEnumerable<char> actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmElementsAreUnique());
	}
	#endregion

	#region ConfirmElementsAreDistinct
	[TestCase(new string[] { "a", "b", "c" }, new string[] { "d", "e", "f" })]
	[TestCase(new string[] { "a", "b", "c", "d" }, new string[] { "e", "f", "g", "h" })]
	[TestCase(new string[] { "a", "b", "c", "d", "e" }, new string[] { "f", "g", "h", "i", "j" })]
	public static void ConfirmElementsAreDistinct_WhenDistinct(IEnumerable<string> actual, IEnumerable<string> expected)
	{
		actual.ConfirmElementsAreDistinct(expected);
	}

	[TestCase(new string[] { "a", "a", "c" }, new string[] { "d", "e", "f" })]
	[TestCase(new string[] { "a", "b", "b", "d" }, new string[] { "e", "f", "g", "h" })]
	[TestCase(new string[] { "a", "b", "c", "c", "e" }, new string[] { "f", "g", "h", "i", "j" })]
	public static void ConfirmElementsAreDistinct_WhenNotDistinct(IEnumerable<string> actual, IEnumerable<string> expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmElementsAreDistinct(expected));
	}
	#endregion

	#region ConfirmElementsAreOrdered
	[TestCase(new int[] { 1, 2, 3 })]
	[TestCase(new int[] { 1, 2, 3, 4 })]
	[TestCase(new int[] { 1, 2, 3, 4, 5 })]
	public static void ConfirmElementsAreOrdered_WhenOrdered(IEnumerable<int> actual)
	{
		actual.ConfirmElementsAreOrdered();
	}

	[TestCase(new int[] { 3, 2, 1 })]
	[TestCase(new int[] { 4, 3, 2, 1 })]
	[TestCase(new int[] { 5, 4, 3, 2, 1 })]
	public static void ConfirmElementsAreOrdered_WhenNotOrdered(IEnumerable<int> actual)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmElementsAreOrdered());
	}
	#endregion

	#region ConfirmElementsAreInRange
	[TestCase(new int[] { 1, 2, 3 }, 1, 3)]
	[TestCase(new int[] { 1, 2, 3, 4 }, 1, 4)]
	[TestCase(new int[] { 1, 2, 3, 4, 5 }, 1, 5)]
	public static void ConfirmElementsAreInRange_WhenInRange(IEnumerable<int> actual, int min, int max)
	{
		actual.ConfirmElementsAreInRange(min, max);
	}

	[TestCase(new int[] { 1, 2, 3 }, 2, 3)]
	[TestCase(new int[] { 1, 2, 3, 4 }, 3, 4)]
	[TestCase(new int[] { 1, 2, 3, 4, 5 }, 4, 5)]
	public static void ConfirmElementsAreInRange_WhenNotInRange(IEnumerable<int> actual, int min, int max)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmElementsAreInRange(min, max));
	}
	#endregion

	#region ConfirmElementsAreEquivalent
	[TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
	[TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 })]
	[TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 })]
	public static void ConfirmElementsAreEquivalent_WhenEquivalent(IEnumerable<int> actual, IEnumerable<int> expected)
	{
		actual.ConfirmElementsAreEquivalent(expected);
	}

	[TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 4 })]
	[TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 5 })]
	[TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 6 })]
	public static void ConfirmElementsAreEquivalent_WhenNotEquivalent_Data(IEnumerable<int> actual, IEnumerable<int> expected)
	{
		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => actual.ConfirmElementsAreEquivalent(expected));
	}
	#endregion
}
