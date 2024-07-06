using System;
using Confirma.Attributes;
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
        array.ConfirmSize(expectedSize);
    }

    [TestCase(new int[] { 1 }, 0)]
    [TestCase(new int[] { 1, 2 }, 1)]
    [TestCase(new int[] { 1, 2, 3 }, 2)]
    public static void ConfirmSize_WhenIsNotOfSize(int[] array, int expectedSize)
    {
        Action action = () => array.ConfirmSize(expectedSize);

        action.ConfirmThrows<ConfirmAssertException>();
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
        Action action = () => array.ConfirmEmpty();

        action.ConfirmThrows<ConfirmAssertException>();
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
        Action action = () => array.ConfirmContains(expected);

        action.ConfirmThrows<ConfirmAssertException>();
    }
}
