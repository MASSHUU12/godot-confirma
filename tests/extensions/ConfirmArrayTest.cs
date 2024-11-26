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
    [TestCase(0)]
    [TestCase(1, 1)]
    [TestCase(2, 1, 2)]
    public void ConfirmSize_WhenIsOfSize(int expectedSize, params int[] array)
    {
        _ = array.ConfirmSize(expectedSize);
    }

    [TestCase(0, 1)]
    [TestCase(1, 1, 2)]
    [TestCase(2, 1, 2, 3)]
    public void ConfirmSize_WhenIsNotOfSize(int expectedSize, params int[] array)
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

    [TestCase(1)]
    [TestCase(1, 2)]
    [TestCase(1, 2, 3)]
    public void ConfirmEmpty_WhenIsNotEmpty(params int[] array)
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

    [TestCase(1, 1)]
    [TestCase(2, 1, 2)]
    [TestCase(2, 1, 2, 3)]
    public void ConfirmContains_WhenContains(int expected, params int[] array)
    {
        _ = array.ConfirmContains(expected);
    }

    [TestCase(2, 1)]
    [TestCase(3, 1, 2)]
    [TestCase(4, 1, 2, 3)]
    public void ConfirmContains_WhenNotContains(int expected, params int[] array)
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
