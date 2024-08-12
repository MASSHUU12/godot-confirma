using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

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
        _ = array.ConfirmSize(expectedSize);
    }

    [TestCase(new int[] { 1 }, 0)]
    [TestCase(new int[] { 1, 2 }, 1)]
    [TestCase(new int[] { 1, 2, 3 }, 2)]
    public static void ConfirmSize_WhenIsNotOfSize(int[] array, int expectedSize)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => array.ConfirmSize(expectedSize));
    }

    [TestCase]
    public static void ConfirmEmpty_WhenIsEmpty()
    {
        _ = Array.Empty<int>().ConfirmEmpty();
    }

    [TestCase(new int[] { 1 })]
    [TestCase(new int[] { 1, 2 })]
    [TestCase(new int[] { 1, 2, 3 })]
    public static void ConfirmEmpty_WhenIsNotEmpty(int[] array)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => array.ConfirmEmpty());
    }

    [TestCase(new int[] { 1 }, 1)]
    [TestCase(new int[] { 1, 2 }, 2)]
    [TestCase(new int[] { 1, 2, 3 }, 2)]
    public static void ConfirmContains_WhenContains(int[] array, int expected)
    {
        _ = array.ConfirmContains(expected);
    }

    [TestCase(new int[] { 1 }, 2)]
    [TestCase(new int[] { 1, 2 }, 3)]
    [TestCase(new int[] { 1, 2, 3 }, 4)]
    public static void ConfirmContains_WhenNotContains(int[] array, int expected)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => array.ConfirmContains(expected));
    }
}
