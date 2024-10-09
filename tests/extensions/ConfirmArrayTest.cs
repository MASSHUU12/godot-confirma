using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmArrayTest
{
    [TestCase(new int[] { }, 0)]
    [TestCase(new int[] { 1 }, 1)]
    [TestCase(new int[] { 1, 2 }, 2)]
    public void ConfirmSize_WhenIsOfSize(int[] array, int expectedSize)
    {
        _ = array.ConfirmSize(expectedSize);
    }

    [TestCase(new int[] { 1 }, 0)]
    [TestCase(new int[] { 1, 2 }, 1)]
    [TestCase(new int[] { 1, 2, 3 }, 2)]
    public void ConfirmSize_WhenIsNotOfSize(int[] array, int expectedSize)
    {
        Action action = () => array.ConfirmSize(expectedSize);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmSize failed: "
            + $"Expected array of size {expectedSize}, but {array.Length} provided."
        );
    }

    [TestCase]
    public void ConfirmEmpty_WhenIsEmpty()
    {
        _ = Array.Empty<int>().ConfirmEmpty();
    }

    [TestCase(new int[] { 1 })]
    [TestCase(new int[] { 1, 2 })]
    [TestCase(new int[] { 1, 2, 3 })]
    public void ConfirmEmpty_WhenIsNotEmpty(int[] array)
    {
        Action action = () => array.ConfirmEmpty();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEmpty failed: "
            + $"Expected empty array, {array.Length} elements provided."
        );
    }

    [TestCase]
    public void ConfirmNotEmpty_WhenNotEmpty()
    {
        _ = new int[] { 1 }.ConfirmNotEmpty();
    }

    [TestCase]
    public void ConfirmNotEmpty_WhenEmpty()
    {
        Action action = static () => Array.Empty<int>().ConfirmNotEmpty();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEmpty failed: Expected non-empty array."
        );
    }

    [TestCase(new int[] { 1 }, 1)]
    [TestCase(new int[] { 1, 2 }, 2)]
    [TestCase(new int[] { 1, 2, 3 }, 2)]
    public void ConfirmContains_WhenContains(int[] array, int expected)
    {
        _ = array.ConfirmContains(expected);
    }

    [TestCase(new int[] { 1 }, 2)]
    [TestCase(new int[] { 1, 2 }, 3)]
    [TestCase(new int[] { 1, 2, 3 }, 4)]
    public void ConfirmContains_WhenNotContains(int[] array, int expected)
    {
        Action action = () => array.ConfirmContains(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmContains failed: "
            + $"Expected array to contain: {expected}."
        );
    }

    [TestCase]
    public void ConfirmNotContains_WhenNotContains()
    {
        _ = new int[] { 1 }.ConfirmNotContains(2);
    }

    [TestCase]
    public void ConfirmNotContains_WhenContains()
    {
        Action action = static () => new int[] { 1 }.ConfirmNotContains(1);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotContains failed: "
            + "Expected array to not contain: 1."
        );
    }
}
