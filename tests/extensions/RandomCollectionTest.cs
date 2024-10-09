using System;
using System.Collections.Generic;
using System.Linq;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class RandomCollectionTest
{
    private static readonly Random _rg = new();

    #region NextElement
    [TestCase(new int[] { 0 })]
    [Repeat(3)]
    [TestCase(new int[] { 0, 1, 2, 3 })]
    public void NextElement_ValidIntArray(int[] elements)
    {
        _ = elements.ConfirmContains(_rg.NextElement(elements));
    }

    [TestCase(new float[] { 0f })]
    [Repeat(3)]
    [TestCase(new float[] { 0f, 1.1f, 2.2f, 3.3f })]
    public void NextElement_ValidStringEnumerable(float[] elements)
    {
        IEnumerable<string> e = elements.Select(static (f) => f.ToString());

        _ = e.ConfirmContains(_rg.NextElement(e));
    }

    [TestCase]
    public void NextElement_InvalidArray()
    {
        Action action = static () => _rg.NextElement(Array.Empty<string>());

        _ = action.ConfirmThrows<InvalidOperationException>();
    }
    #endregion NextElement

    #region NextElements
    [TestCase(new int[] { 0 })]
    [Repeat(3)]
    [TestCase(new int[] { 0, 1, 2, 3 })]
    public void NextElements_ValidIntArray(int[] elements)
    {
        int expectedSize = _rg.Next(1, elements.Length + 1);

        _ = _rg.NextElements(expectedSize, elements).ConfirmCount(expectedSize);
    }

    [TestCase]
    public void NextElements_TakeZeroElements()
    {
        Action action = static () => _rg.NextElements(0, new int[] { 0, 1, 2 });

        _ = action.ConfirmThrows<InvalidOperationException>();
    }

    [Repeat(3)]
    [TestCase]
    public void NextElements_InvalidArray()
    {
        Action action = static () => _rg.NextElements(_rg.Next(), Array.Empty<string>());

        _ = action.ConfirmThrows<InvalidOperationException>();
    }
    #endregion NextElements

    #region NextShuffle
    [Repeat(3)]
    [TestCase]
    public void NextShuffle_ChangesOrderOfElements()
    {
        List<int> originalCollection = new() { 1, 2, 3, 4, 5 };
        IEnumerable<int> shuffledCollection = _rg.NextShuffle(originalCollection);

        _ = shuffledCollection.ConfirmNotEqual(originalCollection);
    }

    [Repeat(3)]
    [TestCase]
    public void NextShuffle_ContainsSameElements()
    {
        List<int> originalCollection = new() { 1, 2, 3, 4, 5 };
        IEnumerable<int> shuffledCollection = _rg.NextShuffle(originalCollection);

        _ = shuffledCollection.ConfirmElementsAreEquivalent(originalCollection);
    }

    [Repeat(3)]
    [TestCase]
    public void NextShuffle_IsRandom()
    {
        List<int> originalCollection = new() { 1, 2, 3, 4, 5 };
        IEnumerable<int> shuffledCollection1 = _rg.NextShuffle(originalCollection);
        IEnumerable<int> shuffledCollection2 = _rg.NextShuffle(originalCollection);

        _ = shuffledCollection1.ConfirmNotEqual(shuffledCollection2);
    }

    [TestCase]
    public void NextShuffle_ThrowsOnEmpty()
    {
        Action action = static () => _rg.NextShuffle(Array.Empty<int>());

        _ = action.ConfirmThrows<InvalidOperationException>();
    }
    #endregion NextShuffle
}
