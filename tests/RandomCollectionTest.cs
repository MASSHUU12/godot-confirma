using System;
using System.Collections.Generic;
using System.Linq;
using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomCollectionTest
{
    private readonly static Random _rg = new();

    #region NextElement
    [TestCase(new int[] { 0 })]
    [Repeat(3)]
    [TestCase(new int[] { 0, 1, 2, 3 })]
    public static void NextElement_ValidIntArray(int[] elements)
    {
        elements.ConfirmContains(_rg.NextElement(elements));
    }

    [TestCase(new float[] { 0f })]
    [Repeat(3)]
    [TestCase(new float[] { 0f, 1.1f, 2.2f, 3.3f })]
    public static void NextElement_ValidStringEnumerable(float[] elements)
    {
        var e = elements.Select((f) => f.ToString());

        e.ConfirmContains(_rg.NextElement(e));
    }

    [TestCase]
    public static void NextElement_InvalidArray()
    {
        Action action = () => _rg.NextElement(Array.Empty<string>());

        action.ConfirmThrows<InvalidOperationException>();
    }
    #endregion

    #region NextElements
    [TestCase(new int[] { 0 })]
    [Repeat(3)]
    [TestCase(new int[] { 0, 1, 2, 3 })]
    public static void NextElements_ValidIntArray(int[] elements)
    {
        int expectedSize = _rg.Next(1, elements.Length + 1);

        _rg.NextElements(expectedSize, elements).ConfirmCount(expectedSize);
    }

    [TestCase]
    public static void NextElements_TakeZeroElements()
    {
        Action action = () => _rg.NextElements(0, new int[] { 0, 1, 2 });

        action.ConfirmThrows<InvalidOperationException>();
    }

    [Repeat(3)]
    [TestCase]
    public static void NextElements_InvalidArray()
    {
        Action action = () => _rg.NextElements(_rg.Next(), Array.Empty<string>());

        action.ConfirmThrows<InvalidOperationException>();
    }
    #endregion

    #region NextShuffle
    [Repeat(3)]
    [TestCase]
    public static void NextShuffle_ChangesOrderOfElements()
    {
        var originalCollection = new List<int> { 1, 2, 3, 4, 5 };
        var shuffledCollection = _rg.NextShuffle(originalCollection);

        shuffledCollection.ConfirmNotEqual(originalCollection);
    }

    [Repeat(3)]
    [TestCase]
    public static void NextShuffle_ContainsSameElements()
    {
        var originalCollection = new List<int> { 1, 2, 3, 4, 5 };
        var shuffledCollection = _rg.NextShuffle(originalCollection);

        shuffledCollection.ConfirmElementsAreEquivalent(originalCollection);
    }

    [Repeat(3)]
    [TestCase]
    public static void NextShuffle_IsRandom()
    {
        var originalCollection = new List<int> { 1, 2, 3, 4, 5 };
        var shuffledCollection1 = _rg.NextShuffle(originalCollection);
        var shuffledCollection2 = _rg.NextShuffle(originalCollection);

        shuffledCollection1.ConfirmNotEqual(shuffledCollection2);
    }

    [TestCase]
    public static void NextShuffle_ThrowsOnEmpty()
    {
        Action action = () => _rg.NextShuffle(Array.Empty<int>());

        action.ConfirmThrows<InvalidOperationException>();
    }
    #endregion
}
