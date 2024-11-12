using System;
using System.Collections.Generic;
using System.Linq;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;
using Confirma.Formatters;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmIEnumerableTest
{
    #region ConfirmEmpty
    [TestCase(new int[] { })]
    public void ConfirmEmpty_WhenEmpty(IEnumerable<int> actual)
    {
        _ = actual.ConfirmEmpty();
    }

    [TestCase(new int[] { 1 })]
    [TestCase(new int[] { 1, 2 })]
    [TestCase(new int[] { 1, 2, 3 })]
    public void ConfirmEmpty_WhenNotEmpty(IEnumerable<int> actual)
    {
        Action action = () => actual.ConfirmEmpty();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEmpty failed: "
            + $"Expected empty enumerable, but found {actual.Count()} elements."
        );
    }
    #endregion ConfirmEmpty

    #region ConfirmNotEmpty
    [TestCase(new int[] { 1 })]
    [TestCase(new int[] { 1, 2 })]
    [TestCase(new int[] { 1, 2, 3 })]
    public void ConfirmNotEmpty_WhenNotEmpty(IEnumerable<int> actual)
    {
        _ = actual.ConfirmNotEmpty();
    }

    [TestCase(new int[] { })]
    public void ConfirmNotEmpty_WhenEmpty(IEnumerable<int> actual)
    {
        Action action = () => actual.ConfirmNotEmpty();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEmpty failed: Expected non-empty enumerable."
        );
    }
    #endregion ConfirmNotEmpty

    #region ConfirmCount
    [TestCase(new float[] { }, 0)]
    [TestCase(new float[] { 1.0f }, 1)]
    [TestCase(new float[] { 1.0f, 2.0f }, 2)]
    [TestCase(new float[] { 1.0f, 2.0f, 3.0f }, 3)]
    public void ConfirmCount_WhenEqual(
        IEnumerable<float> actual,
        int expected
    )
    {
        _ = actual.ConfirmCount(expected);
    }

    [TestCase(new float[] { }, 1)]
    [TestCase(new float[] { 1.0f }, 0)]
    [TestCase(new float[] { 1.0f, 2.0f }, 1)]
    [TestCase(new float[] { 1.0f, 2.0f, 3.0f }, 2)]
    public void ConfirmCount_WhenNotEqual(
        IEnumerable<float> actual,
        int expected
    )
    {
        Action action = () => actual.ConfirmCount(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmCount failed: "
            + $"Expected enumerable with {expected} elements, "
            + $"but found {actual.Count()}."
        );
    }
    #endregion ConfirmCount

    #region ConfirmCountGreaterThan
    [TestCase(new double[] { 1.0 }, 0)]
    [TestCase(new double[] { 1.0, 2.0 }, 1)]
    [TestCase(new double[] { 1.0, 2.0, 3.0 }, 2)]
    public void ConfirmCountGreaterThan_WhenGreaterThan(
        IEnumerable<double> actual,
        int expected
    )
    {
        _ = actual.ConfirmCountGreaterThan(expected);
    }

    [TestCase(new double[] { }, 0)]
    [TestCase(new double[] { 1.0 }, 1)]
    [TestCase(new double[] { 1.0, 2.0 }, 2)]
    [TestCase(new double[] { 1.0, 2.0, 3.0 }, 3)]
    public void ConfirmCountGreaterThan_WhenNotGreaterThan(
        IEnumerable<double> actual,
        int expected
    )
    {
        Action action = () => actual.ConfirmCountGreaterThan(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmCountGreaterThan failed: "
            + $"Expected enumerable with more than {expected} elements, "
            + $"but found {actual.Count()}."
        );
    }
    #endregion ConfirmCountGreaterThan

    #region ConfirmCountLessThan
    [TestCase(new short[] { }, 1)]
    [TestCase(new short[] { 1 }, 2)]
    [TestCase(new short[] { 1, 2 }, 3)]
    public void ConfirmCountLessThan_WhenLessThan(
        IEnumerable<short> actual,
        int expected
    )
    {
        _ = actual.ConfirmCountLessThan(expected);
    }

    [TestCase(new short[] { 1 }, 0)]
    [TestCase(new short[] { 1, 2 }, 1)]
    [TestCase(new short[] { 1, 2, 3 }, 2)]
    [TestCase(new short[] { 1, 2, 3, 4 }, 3)]
    public void ConfirmCountLessThan_WhenNotLessThan(
        IEnumerable<short> actual,
        int expected
    )
    {
        Action action = () => actual.ConfirmCountLessThan(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmCountLessThan failed: "
            + $"Expected enumerable with fewer than {expected} elements, "
            + $"but found {actual.Count()}."
        );
    }
    #endregion ConfirmCountLessThan

    #region ConfirmCountGreaterThanOrEqual
    [TestCase(new int[] { }, 0)]
    [TestCase(new int[] { 1 }, 0)]
    [TestCase(new int[] { 1, 2 }, 1)]
    [TestCase(new int[] { 1, 2, 3 }, 2)]
    public void ConfirmCountGreaterThanOrEqual_WhenGreaterThanOrEqual(
        IEnumerable<int> actual,
        int expected
    )
    {
        _ = actual.ConfirmCountGreaterThanOrEqual(expected);
    }

    [TestCase(new int[] { }, 1)]
    [TestCase(new int[] { 1 }, 2)]
    [TestCase(new int[] { 1, 2 }, 3)]
    [TestCase(new int[] { 1, 2, 3 }, 4)]
    public void ConfirmCountGreaterThanOrEqual_WhenNotGreaterThanOrEqual(
        IEnumerable<int> actual,
        int expected
    )
    {
        Action action = () => actual.ConfirmCountGreaterThanOrEqual(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmCountGreaterThanOrEqual failed: "
            + $"Expected enumerable with at least {expected} elements, "
            + $"but found {actual.Count()}."
        );
    }
    #endregion ConfirmCountGreaterThanOrEqual

    #region ConfirmCountLessThanOrEqual
    [TestCase(new long[] { }, 1)]
    [TestCase(new long[] { 1 }, 1)]
    [TestCase(new long[] { 1, 2 }, 2)]
    [TestCase(new long[] { 1, 2, 3 }, 3)]
    public void ConfirmCountLessThanOrEqual_WhenLessThanOrEqual(
        IEnumerable<long> actual,
        int expected
    )
    {
        _ = actual.ConfirmCountLessThanOrEqual(expected);
    }

    [TestCase(new long[] { 1 }, 0)]
    [TestCase(new long[] { 1, 2 }, 1)]
    [TestCase(new long[] { 1, 2, 3 }, 2)]
    [TestCase(new long[] { 1, 2, 3, 4 }, 3)]
    public void ConfirmCountLessThanOrEqual_WhenNotLessThanOrEqual(
        IEnumerable<long> actual,
        int expected
    )
    {
        Action action = () => actual.ConfirmCountLessThanOrEqual(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmCountLessThanOrEqual failed: "
            + $"Expected enumerable with at most {expected} elements, "
            + $"but found {actual.Count()}."
        );
    }
    #endregion ConfirmCountLessThanOrEqual

    #region ConfirmContains
    [TestCase(new string[] { "a", "b", "c" }, "a")]
    [TestCase(new string[] { "a", "b", "c" }, "b")]
    [TestCase(new string[] { "a", "b", "c" }, "c")]
    public void ConfirmContains_WhenContains(
        IEnumerable<string> actual,
        string expected
    )
    {
        _ = actual.ConfirmContains(expected);
    }

    [TestCase(new string[] { "a", "b", "c" }, "d")]
    [TestCase(new string[] { "a", "b", "c" }, "e")]
    [TestCase(new string[] { "a", "b", "c" }, "f")]
    public void ConfirmContains_WhenNotContains(
        IEnumerable<string> actual,
        string expected
    )
    {
        Action action = () => actual.ConfirmContains(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmContains failed: "
            + "Expected enumerable to contain "
            + $"{new StringFormatter().Format(expected)}."
        );
    }
    #endregion ConfirmContains

    #region ConfirmNotContains
    [TestCase(new string[] { "a", "b", "c" }, "d")]
    [TestCase(new string[] { "a", "b", "c" }, "e")]
    [TestCase(new string[] { "a", "b", "c" }, "f")]
    public void ConfirmNotContains_WhenNotContains(
        IEnumerable<string> actual,
        string expected
    )
    {
        _ = actual.ConfirmNotContains(expected);
    }

    [TestCase(new string[] { "a", "b", "c" }, "a")]
    [TestCase(new string[] { "a", "b", "c" }, "b")]
    [TestCase(new string[] { "a", "b", "c" }, "c")]
    public void ConfirmNotContains_WhenContains(
        IEnumerable<string> actual,
        string expected
    )
    {
        Action action = () => actual.ConfirmNotContains(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotContains failed: "
            + "Expected enumerable to not contain "
            + $"{new StringFormatter().Format(expected)}."
        );
    }
    #endregion ConfirmNotContains

    #region ConfirmAllMatch
    [TestCase(new int[] { 1, 2, 3 })]
    [TestCase(new int[] { 1, 2, 3, 4 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5 })]
    public void ConfirmAllMatch_WhenAllMatch(IEnumerable<int> actual)
    {
        _ = actual.ConfirmAllMatch(x => x > 0);
    }

    [TestCase(new int[] { 1, 2, 3 })]
    [TestCase(new int[] { 1, 2, 3, 4 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5 })]
    public void ConfirmAllMatch_WhenNotAllMatch(IEnumerable<int> actual)
    {
        Action action = () => actual.ConfirmAllMatch(x => x < 0);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmAllMatch failed: "
            + "Expected elements to match the predicate."
        );
    }
    #endregion ConfirmAllMatch

    #region ConfirmAnyMatch
    [TestCase(new int[] { 1, 2, 3 })]
    [TestCase(new int[] { 1, 2, 3, 4 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5 })]
    public void ConfirmAnyMatch_WhenAnyMatch(IEnumerable<int> actual)
    {
        _ = actual.ConfirmAnyMatch(x => x == 3);
    }

    [TestCase(new int[] { 1, 2, 3 })]
    [TestCase(new int[] { 1, 2, 3, 4 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5 })]
    public void ConfirmAnyMatch_WhenNotAnyMatch(IEnumerable<int> actual)
    {
        Action action = () => actual.ConfirmAnyMatch(x => x < 0);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmAnyMatch failed: "
            + "Expected at least one element to match the predicate."
        );
    }
    #endregion ConfirmAnyMatch

    #region ConfirmNoneMatch
    [TestCase(new float[] { 1.0f, 2.0f, 3.0f })]
    [TestCase(new float[] { 1.0f, 2.0f, 3.0f, 4.0f })]
    [TestCase(new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f })]
    public void ConfirmNoneMatch_WhenNoneMatch(IEnumerable<float> actual)
    {
        _ = actual.ConfirmNoneMatch(x => x < 0);
    }

    [TestCase(new float[] { 1.0f, 2.0f, 3.0f })]
    [TestCase(new float[] { 1.0f, 2.0f, 3.0f, 4.0f })]
    [TestCase(new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f })]
    public void ConfirmNoneMatch_WhenNotNoneMatch(IEnumerable<float> actual)
    {
        Action action = () => actual.ConfirmNoneMatch(x => x > 0);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNoneMatch failed: "
            + "Expected no elements to match the predicate."
        );
    }
    #endregion ConfirmNoneMatch

    #region ConfirmElementsAreUnique
    [TestCase(new char[] { 'a', 'b', 'c' })]
    [TestCase(new char[] { 'a', 'b', 'c', 'd' })]
    [TestCase(new char[] { 'a', 'b', 'c', 'd', 'e' })]
    public void ConfirmElementsAreUnique_WhenUnique(IEnumerable<char> actual)
    {
        _ = actual.ConfirmElementsAreUnique();
    }

    [TestCase(new char[] { 'a', 'a', 'c' })]
    [TestCase(new char[] { 'a', 'b', 'b', 'd' })]
    [TestCase(new char[] { 'a', 'b', 'c', 'c', 'e' })]
    public void ConfirmElementsAreUnique_WhenNotUnique(IEnumerable<char> actual)
    {
        Action action = () => actual.ConfirmElementsAreUnique();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmElementsAreUnique failed: "
            + "Expected elements to be unique."
        );
    }
    #endregion ConfirmElementsAreUnique

    #region ConfirmElementsAreDistinct
    [TestCase(new string[] { "a", "b", "c" }, new string[] { "d", "e", "f" })]
    [TestCase(new string[] { "a", "b", "c", "d" }, new string[] { "e", "f", "g", "h" })]
    [TestCase(new string[] { "a", "b", "c", "d", "e" }, new string[] { "f", "g", "h", "i", "j" })]
    public void ConfirmElementsAreDistinct_WhenDistinct(
        IEnumerable<string> actual,
        IEnumerable<string> expected
    )
    {
        _ = actual.ConfirmElementsAreDistinct(expected);
    }

    [TestCase(new string[] { "a", "a", "c" }, new string[] { "d", "e", "f" })]
    [TestCase(new string[] { "a", "b", "b", "d" }, new string[] { "e", "f", "g", "h" })]
    [TestCase(new string[] { "a", "b", "c", "c", "e" }, new string[] { "f", "g", "h", "i", "j" })]
    public void ConfirmElementsAreDistinct_WhenNotDistinct(
        IEnumerable<string> actual,
        IEnumerable<string> expected
    )
    {
        Action action = () => actual.ConfirmElementsAreDistinct(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmElementsAreDistinct failed: "
            + "Expected elements to be distinct from the expected set."
        );
    }
    #endregion ConfirmElementsAreDistinct

    #region ConfirmElementsAreOrdered
    [TestCase(new int[] { 1, 2, 3 })]
    [TestCase(new int[] { 1, 2, 3, 4 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5 })]
    public void ConfirmElementsAreOrdered_WhenOrdered(IEnumerable<int> actual)
    {
        _ = actual.ConfirmElementsAreOrdered();
    }

    [TestCase(new int[] { 3, 2, 1 })]
    [TestCase(new int[] { 4, 3, 2, 1 })]
    [TestCase(new int[] { 5, 4, 3, 2, 1 })]
    public void ConfirmElementsAreOrdered_WhenNotOrdered(IEnumerable<int> actual)
    {
        Action action = () => actual.ConfirmElementsAreOrdered();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmElementsAreOrdered failed: "
            + "Expected elements to be in order."
        );
    }

    [TestCase]
    public void ConfirmElementsAreOrdered_Ordered_ReturnsOriginalSequence()
    {
        int[] actual = new[] { 1, 2, 3 };
        IEnumerable<int> result = actual.ConfirmElementsAreOrdered(
            Comparer<int>.Default
        );

        _ = actual.ConfirmEqual(result);
    }

    [TestCase]
    public void ConfirmElementsAreOrdered_Unordered_ThrowsConfirmAssertException()
    {
        Action action = static () => new[] { 3, 2, 1 }
            .ConfirmElementsAreOrdered(Comparer<int>.Default);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmElementsAreOrdered failed: "
            + "Expected elements to be in order."
        );
    }

    [TestCase]
    public void ConfirmElementsAreOrdered_CustomComparer_ReturnsOriginalSequence()
    {
        string[] actual = new[] { "a", "b", "c" };
        IEnumerable<string> result = actual.ConfirmElementsAreOrdered(
            StringComparer.OrdinalIgnoreCase
        );

        _ = actual.ConfirmEqual(result);
    }

    [TestCase]
    public void ConfirmElementsAreOrdered_CustomComparer_ThrowsConfirmAssertException()
    {
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        Action action = static () => new[] { "a", "c", "b" }
            .ConfirmElementsAreOrdered(StringComparer.OrdinalIgnoreCase);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmElementsAreOrdered failed: "
            + "Expected elements to be in order."
        );
    }
    #endregion ConfirmElementsAreOrdered

    #region ConfirmElementsAreInRange
    [TestCase(new int[] { 1, 2, 3 }, 1, 3)]
    [TestCase(new int[] { 1, 2, 3, 4 }, 1, 4)]
    [TestCase(new int[] { 1, 2, 3, 4, 5 }, 1, 5)]
    public void ConfirmElementsAreInRange_WhenInRange(
        IEnumerable<int> actual,
        int min,
        int max
    )
    {
        _ = actual.ConfirmElementsAreInRange(min, max);
    }

    [TestCase(new int[] { 1, 2, 3 }, 2, 3)]
    [TestCase(new int[] { 1, 2, 3, 4 }, 3, 4)]
    [TestCase(new int[] { 1, 2, 3, 4, 5 }, 4, 5)]
    public void ConfirmElementsAreInRange_WhenNotInRange(
        IEnumerable<int> actual,
        int min,
        int max
    )
    {
        Action action = () => actual.ConfirmElementsAreInRange(min, max);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmElementsAreInRange failed: "
            + $"Expected elements to be within the range [{min}, {max}]."
        );
    }
    #endregion ConfirmElementsAreInRange

    #region ConfirmElementsAreEquivalent
    [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
    [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 })]
    public void ConfirmElementsAreEquivalent_WhenEquivalent(
        IEnumerable<int> actual,
        IEnumerable<int> expected
    )
    {
        _ = actual.ConfirmElementsAreEquivalent(expected);
    }

    [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 4 })]
    [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 5 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 6 })]
    public void ConfirmElementsAreEquivalent_WhenNotEquivalent_Data(
        IEnumerable<int> actual,
        IEnumerable<int> expected
    )
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => actual.ConfirmElementsAreEquivalent(expected)
        );

        Action action = () => actual.ConfirmElementsAreEquivalent(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmElementsAreEquivalent failed: "
            + "Expected elements to be equivalent to the expected set."
        );
    }
    #endregion ConfirmElementsAreEquivalent
}
