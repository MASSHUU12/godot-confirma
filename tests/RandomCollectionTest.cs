using System;
using System.Linq;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomCollectionTest
{
    private readonly static Random _rg = new();

    [TestCase(new int[] { 0 })]
    [TestCase(new int[] { 0, 1, 2, 3 })]
    public static void NextElement_ValidIntArray(int[] elements)
    {
        elements.ConfirmContains(_rg.NextElement(elements));
    }

    [TestCase(new float[] { 0f })]
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
}
